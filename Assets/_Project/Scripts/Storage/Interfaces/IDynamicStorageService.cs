using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageService
{
    public interface IDynamicStorageService // Database
    {
        Task Download(Dictionary<string, string> param, Action<PlayerData> callback);
        Task Upload(Dictionary<string, string> key, Action<bool> callback = null);
    }
}