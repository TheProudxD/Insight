using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleJSON;

namespace StorageService
{
    public interface IDynamicStorageService // Database
    {
        Task<JSONNode> Download(Dictionary<string, string> param, Action<bool> callback=null);
        Task Upload(Dictionary<string, string> key, Action<bool> callback = null);
    }
}