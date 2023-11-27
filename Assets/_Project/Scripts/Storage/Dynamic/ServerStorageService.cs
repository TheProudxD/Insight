using SimpleJSON;
using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace StorageService
{
    public class ServerStorageService : IDynamicStorageService
    {
        private readonly string _url;

        public ServerStorageService(string url) => _url = url;

        public async Task Load(string key, Action<DynamicData> callback = null)
        {
            using var wc = new WebClient();
            try
            {
                var json = await wc.DownloadStringTaskAsync(_url + $"/api.php?action={key}");

                var data = JSONNode.Parse(json);

                if (data is null)
                    throw new NullReferenceException("JSON string is null");

                var dynamicData = new DynamicData
                {
                    AmountHardResources = data["user"]["hardcurrency"],
                    AmountSoftResources = data["user"]["softcurrency"],
                    CurrentLevel = data["user"]["level"],
                };
                
                callback?.Invoke(dynamicData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        public void Save(string key, object data, Action<bool> callback = null)
        {
            Debug.Log("Saved to the server..");
            //throw new NotImplementedException();
        }
    }
}