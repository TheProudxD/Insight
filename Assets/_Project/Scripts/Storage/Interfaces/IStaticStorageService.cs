using System;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IStaticStorageService // JSON
    {
        Task Download(string key, Action<StaticPlayerData> callback);
        void Upload(string key, object data, Action<bool> callback = null);
    }
}