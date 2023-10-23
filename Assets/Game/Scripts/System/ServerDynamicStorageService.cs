using ResourceService;
using SimpleJSON;
using StorageService;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.System
{
    public class ServerDynamicStorageService: IStorageService
    {
        private readonly string _url;

        public ServerDynamicStorageService(string url)
        {
            _url = url;
        }

        public async Task Load<T>(string key, Action<T> callback=null)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var json = await wc.DownloadStringTaskAsync(_url + @$"/api.php?action={key}");

                    var data = JSONNode.Parse(json);

                    if (data is null) 
                        throw new NullReferenceException("JSON string is null");

                    ResourceManager.Instance.Add(ResourceType.HardCurrency, data["user"]["hardcurrency"]);
                    ResourceManager.Instance.Add(ResourceType.SoftCurrency, data["user"]["softcurrency"]);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        public void Save(string key, object data, Action<bool> callback = null)
        {
            throw new NotImplementedException();
        }
    }
}
