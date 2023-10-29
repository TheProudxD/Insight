using System;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IDynamicStorageService // Database
    {
        void Save(string key, object data, Action<bool> callback = null);
        Task Load(string key, Action<DynamicData> callback);
    }
}