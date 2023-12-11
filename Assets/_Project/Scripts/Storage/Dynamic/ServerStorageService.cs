using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace StorageService
{
    public class ServerStorageService: IDynamicStorageService
    {
        private readonly string _url;

        public ServerStorageService(string url) => _url = url;

        public async Task Download(Dictionary<string, string> param, Action<DynamicData> callback = null)
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

                var callbackData = new DynamicData
                {
                    AmountHardResources = data["user"]["HardCurrency"],
                    AmountSoftResources = data["user"]["SoftCurrency"],
                    CurrentLevel = data["user"]["lvl"],
                    Name = data["user"]["Name"],
                };
                
                callback?.Invoke(callbackData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        private string CreateQuery(Dictionary<string, string> dictionary)
        {
            var result = "";
            foreach (var item in dictionary) 
                result += item.Key + "=" + item.Value + "&";
            return result;
        }

        public async void Upload(string key, object data, Action<bool> callback = null)
        {
            Debug.Log("Saving to the server..");

            using var wc = new WebClient();
            try
            {
                var path = _url + $"/api.php?{key}";
                var json = await wc.DownloadStringTaskAsync(path);
                var data = JSONNode.Parse(json);

                //wc.UploadDataAsync(new Uri(url));

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