using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IDynamicStorageService // Database
    {
        void Upload(string key, object data, Action<bool> callback = null);
        Task Download(Dictionary<string, string> param, Action<DynamicData> callback);
    }
}