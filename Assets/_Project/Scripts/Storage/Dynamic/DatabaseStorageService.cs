using System;
using System.Net;
using System.Threading.Tasks;

namespace StorageService
{
    public class DatabaseStorageService : IDynamicStorageService
    {
        private readonly string _url;

        public DatabaseStorageService(string url) => _url = url;

        public Task Load(string key, Action<DynamicData> callback = null)
        {
            throw new NotImplementedException();
        }

        public void Save(string key, object data, Action<bool> callback = null)
        {
            throw new NotImplementedException();
        }
    }
}