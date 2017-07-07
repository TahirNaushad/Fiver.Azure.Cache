using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiver.Azure.Cache
{
    public interface IAzureCacheStorage
    {
        Task SetStringAsync(string key, string value);
        Task SetObjectAsync(string key, object value);
        Task<string> GetStringAsync(string key);
        Task<T> GetObjectAsync<T>(string key);
        Task<bool> ExistAsync(string key);
        Task DeleteAsync(string key);
        List<string> ListKeys();
        Dictionary<string, string> ListKeyValues();
    }
}
