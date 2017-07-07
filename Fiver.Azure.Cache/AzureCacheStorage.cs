using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiver.Azure.Cache
{
    public class AzureCacheStorage : IAzureCacheStorage
    {
        #region " Public "

        public AzureCacheStorage(AzureCacheSettings settings)
        {
            this.settings = settings;
            Init();
        }
        
        public async Task SetStringAsync(string key, string value)
        {
            await database.StringSetAsync(key, value);
        }

        public async Task SetObjectAsync(string key, object value)
        {
            await database.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<string> GetStringAsync(string key)
        {
            var value = await database.StringGetAsync(key);
            return value.IsNullOrEmpty ? "" : value.ToString();
        }

        public async Task<T> GetObjectAsync<T>(string key)
        {
            var value = await database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<bool> ExistAsync(string key)
        {
            return await database.KeyExistsAsync(key);
        }

        public async Task DeleteAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }

        public List<string> ListKeys()
        {
            var list = new List<string>();
            foreach (var item in server.Keys())
            {
                list.Add(item.ToString());
            }
            return list;
        }

        public Dictionary<string, string> ListKeyValues()
        {
            var list = new Dictionary<string, string>();
            var keys = ListKeys();
            foreach (var item in keys)
            {
                if (database.KeyType(item) == RedisType.String)
                    list.Add(item.ToString(), database.StringGet(item));
            }
            return list;
        }

        #endregion

        #region " Private "

        private AzureCacheSettings settings;
        private ConnectionMultiplexer connection;
        private IDatabase database;
        private IServer server;

        private void Init()
        {
            connection = ConnectionMultiplexer.Connect(settings.ConnectionString);
            database = connection.GetDatabase();
            server = connection.GetServer(connection.GetEndPoints().First());
        }

        #endregion
    }
}
