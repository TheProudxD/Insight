using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace StorageService
{
    public class ServerStorageService : IDynamicStorageService
    {
        private readonly string _url;

        public ServerStorageService(string url) => _url = url;

        public async Task Download(Dictionary<string, string> param, Action<PlayerData> callback = null)
        {
            using var wc = new WebClient();
            try
            {
                var query = CreateQuery(param);
                var path = _url + $"/api.php?{query}";
                var json = await wc.DownloadStringTaskAsync(path);
                var data = JSONNode.Parse(json);

                if (data is null)
                    throw new NullReferenceException("JSON string is null");

                var callbackData = new PlayerData
                {
                    AmountHardResources = data["HardCurrency"],
                    AmountSoftResources = data["SoftCurrency"],
                    CurrentLevel = data["lvl"],
                    Name = data["Name"],
                };

                callback?.Invoke(callbackData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message + exception.StackTrace);
            }
        }

        private string CreateQuery(Dictionary<string, string> dictionary)
        {
            var result = "";
            foreach (var item in dictionary)
                result += item.Key + "=" + item.Value + "&";
            return result;
        }

        public async Task Upload(Dictionary<string, string> param, Action<bool> callback = null)
        {
            using var wc = new WebClient();
            try
            {
                var query = CreateQuery(param);
                var path = _url + $"/api.php?{query}";
                var bytes = await wc.UploadDataTaskAsync(path, new byte[]{1});
                callback?.Invoke(true);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
                callback?.Invoke(false);
            }
        }
    }
}