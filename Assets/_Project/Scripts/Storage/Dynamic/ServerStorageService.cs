using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StorageService
{
    public class ServerStorageService : IDynamicStorageService
    {
        private readonly string _url;

        public ServerStorageService(string url) => _url = url;

        public async Task<JSONNode> Download(Dictionary<string, string> param, Action<bool> callback)
        {
            using var wc = new WebClient();
            try
            {
                var query = CreateQuery(param);
                var path = _url + $"/api.php?{query}";
                var json = await wc.DownloadStringTaskAsync(path);
                var data = JSONNode.Parse(json);

                if (data is null)
                {
                    callback?.Invoke(false);
                    throw new NullReferenceException("data is null");
                }
                
                callback?.Invoke(true);
                return data;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + exception.StackTrace);
            }
        }

        public async Task Upload(Dictionary<string, string> param, Action<bool> callback = null)
        {
            using var wc = new WebClient();
            try
            {
                var query = CreateQuery(param);
                var path = _url + $"/api.php?{query}";
                var bytes = await wc.UploadDataTaskAsync(path, new byte[]{1});
                // TODO: ...
                callback?.Invoke(true);
            }
            catch (Exception exception)
            {
                callback?.Invoke(false);
                throw new Exception(exception.Message);
            }
        }

        private string CreateQuery(Dictionary<string, string> dictionary)
        {
            var result = "";
            foreach (var item in dictionary)
                result += item.Key + "=" + item.Value + "&";
            return result;
        }
    }
}