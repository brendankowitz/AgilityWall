using System.Threading.Tasks;

namespace AgilityWall.Core.Infrastructure
{
    public interface IObjectStorageService<T> where T : class
    {
        Task DeleteAsync(string key);
        Task<T> LoadAsync(string key);
        Task SaveAsync(string key, T obj);
        string CreateStorageKey(string key);
        void ClearStorage();
    }
}