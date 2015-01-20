using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using AgilityWall.Core.Infrastructure;
using Newtonsoft.Json;

namespace AgilityWall.WinStore.Infrastructure.PlatformServices
{
    public class ObjectStorageService<T> : IObjectStorageService<T> where T : class
    {
        private string _objectstoragePath = "TempObjectStorage";
        readonly JsonSerializer _serializer = new JsonSerializer();

        // ReSharper disable once StaticFieldInGenericType
        static readonly SemaphoreSlim WriteLock = new SemaphoreSlim(1, 1);

        private StorageFolder StorageFolder { get; set; }

        public string ObjectstoragePath
        {
            get { return _objectstoragePath; }
            set { _objectstoragePath = value; }
        }

        public string CreateStorageKey(string key)
        {
            return Path.Combine(ObjectstoragePath, string.Format("{0}.json", key));
        }

        public ObjectStorageService(StorageFolder storageLocation)
        {
            StorageFolder = storageLocation;
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                await WriteLock.WaitAsync();
                var file = await StorageFolder.GetFileAsync(key);
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (FileNotFoundException)
            {
                // doesn't matter if we attempt to delete a file that does not exist
            }
            finally
            {
                WriteLock.Release();
            }
        }

        public async Task<T> LoadAsync(string key)
        {
            Task deleteTask = null;
            try
            {
                await WriteLock.WaitAsync();
                var file = await StorageFolder.GetFileAsync(key);
                using (var stream = new StreamReader(await file.OpenStreamForReadAsync()))
                using (var jsonReader = new JsonTextReader(stream))
                {
                    var jsonStream = jsonReader;
                    return await Task.Run(() => _serializer.Deserialize<T>(jsonStream));
                }
            }
            catch (FileNotFoundException)
            {
                // file not found is perfectly valid so simply return the default 
            }
            catch
            {
                deleteTask = StorageFolder.GetFileAsync(key).AsTask()
// ReSharper disable once EmptyGeneralCatchClause
                                          .ContinueWith(async x => {
                                                                 try
                                                                 {
                                                                     await x.Result.DeleteAsync();
                                                                 } catch { } });
            }
            finally
            {
                WriteLock.Release();
            }
            if (deleteTask != null) await deleteTask;
            return default(T);
        }

        public async Task SaveAsync(string key, T obj)
        {
            try
            {
                await WriteLock.WaitAsync();
                var path = key.Split('\\', '/');
                var pathBuilder = string.Empty;
                for (var i = 0; i < path.Length - 1; i++)
                {
                    pathBuilder = Path.Combine(pathBuilder, path[i]);
                    if (await DirectoryExists(pathBuilder) == false)
                    {
                        await StorageFolder.CreateFolderAsync(pathBuilder);
                    }
                }

                if (obj == null) return;
                var file = await StorageFolder.CreateFileAsync(key, CreationCollisionOption.ReplaceExisting);
                using (var stream = new StreamWriter(await file.OpenStreamForWriteAsync()))
                using (var jsonWriter = new JsonTextWriter(stream))
                {
                    _serializer.Serialize(jsonWriter, obj);
                    await stream.FlushAsync();
                }
            }
            finally
            {
                WriteLock.Release();
            }
        }

        public void ClearStorage()
        {
            ClearDirectory(ObjectstoragePath);
        }

        async void ClearDirectory(string directoryName)
        {
            try
            {
                await WriteLock.WaitAsync();
                if (!string.IsNullOrEmpty(directoryName) && await DirectoryExists(directoryName))
                {
                    var dir = await StorageFolder.GetFolderAsync(directoryName);
                    var fn = await dir.GetFilesAsync();
                    if (fn.Count > 0)
                        foreach (var t in fn)
                            await t.DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (Debugger.IsAttached) Debugger.Break();
            }
            finally
            {
                WriteLock.Release();
            }
        }

        async Task<bool> DirectoryExists(string dirName)
        {
            try
            {
                await StorageFolder.GetFolderAsync(dirName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}