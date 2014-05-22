using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AgilityWall.Core.Infrastructure;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;

namespace AgilityWall.Core.PlatformServices
{
    public class ObjectStorageService<T> : IObjectStorageService<T>, IDisposable where T : class
    {
        private const string ObjectstoragePath = "TempObjectStorage";
        private readonly SemaphoreSlim _sync = new SemaphoreSlim(1, 1);
        private readonly IsolatedStorageFile _storage = IsolatedStorageFile.GetUserStoreForApplication();

        public void Dispose()
        {
            _storage.Dispose();
        }

        public string CreateStorageKey(string key)
        {
            var path = Path.Combine(ObjectstoragePath, string.Format("{0}.json", key));
            return path;
        }

        public Task DeleteAsync(string key)
        {
            if (_storage.FileExists(key))
                _storage.DeleteFile(key);

            return Task.FromResult(0);
        }

        public async Task<T> LoadAsync(string key)
        {
            await _sync.WaitAsync();
            T retval = null;
            try
            {
                if (_storage.FileExists(key))
                {
                    using (var file = _storage.OpenFile(key, FileMode.Open))
                    {
                        using (var f = new StreamReader(file))
                        {
                            var val = await f.ReadToEndAsync();
#if DEBUG
                            Debug.WriteLine("Loading " + key + "; " + val);
#endif
                            retval = JsonConvert.DeserializeObject<T>(val);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    _storage.DeleteFile(key);
                }
// ReSharper disable once EmptyGeneralCatchClause
                catch { }
                Debug.WriteLine(ex.Message);
                if (Debugger.IsAttached) Debugger.Break();
            }
            finally
            {
                _sync.Release();
            }
            return retval;
        }

        public async Task SaveAsync(string key, T obj)
        {
            await _sync.WaitAsync();
            try
            {
                var path = key.Split('\\', '/');
                var pathBuilder = string.Empty;
                for (var i = 0; i < path.Length - 1; i++)
                {
                    pathBuilder = Path.Combine(pathBuilder, path[i]);
                    if (_storage.DirectoryExists(pathBuilder) == false)
                    {
                        _storage.CreateDirectory(pathBuilder);
                    }
                }

                using (var isolatedStorageFileStream = _storage.OpenFile(key, FileMode.Create))
                using (var file = new StreamWriter(isolatedStorageFileStream))
                {
                    var serialized = JsonConvert.SerializeObject(obj);
                    await file.WriteAsync(serialized);
#if DEBUG
                    Debug.WriteLine("Serializing to " + key + "; " + serialized);
#endif
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                _sync.Release();
            }
        }

        public void ClearStorage()
        {
            ClearDirectory(ObjectstoragePath);
        }

        private void ClearDirectory(string directoryName)
        {
            _sync.Wait();
            try
            {
                if (!string.IsNullOrEmpty(directoryName) && _storage.DirectoryExists(directoryName))
                {
                    var files = _storage.GetFileNames(Path.Combine(directoryName, "*"));
                    if (files.Length > 0)
                        foreach (string item in files)
                            _storage.DeleteFile(Path.Combine(directoryName, item));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                _sync.Release();
            }
        }
    }
}