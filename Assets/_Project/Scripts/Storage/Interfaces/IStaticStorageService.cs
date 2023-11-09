using System;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IStaticStorageService // JSON
    {
        void Save(string key, object data, Action<bool> callback = null);
        Task Load(string key, Action<StaticData> callback);
    }
}