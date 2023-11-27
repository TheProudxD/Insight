using System;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IDynamicStorageService // Database
    {
        void Upload(string key, object data, Action<bool> callback = null);
        Task Download(string key, Action<DynamicData> callback);
    }
}