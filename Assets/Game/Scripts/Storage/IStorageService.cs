using System;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IStorageService
    {
        void Save(string key, object data, Action<bool> callback = null);
        Task Load(string key, Action<JSONData> callback);
    }
    
    public interface ITransferService
    {
        void Save(string key, object data, Action<bool> callback = null);
        Task Load(string key, Action<DynamicData> callback);
    }
}