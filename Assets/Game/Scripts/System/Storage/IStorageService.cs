using System;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IStorageService
    {
        void Save(string key, object data, Action<bool> callback = null);
        Task Load<T>(string key, Action<T> callback);
    }
}