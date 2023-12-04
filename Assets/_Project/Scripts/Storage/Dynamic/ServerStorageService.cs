using Assets._Project.Scripts.Storage.Static;
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

        public async Task Download(string n, Action<DynamicData> callback = null)
        {
            var param = new Dictionary<string, string>();
            param.Add("action", "alldata");
            param.Add("playerid", SystemData.Instance.UID.ToString());
            var data = SendQuary(param, (data)=> 
            { 
                if (callback!=null)
                    callback(new DynamicData
                        {
                            AmountHardResources = data["user"]["HardCurrency"],
                            AmountSoftResources = data["user"]["SoftCurrency"],
                            CurrentLevel = data["user"]["lvl"],
                        });
               
            });
        }

         public async Task SendQuary(Dictionary<string, string> @params, Action<JSONNode> callback = null)
        {
            using var wc = new WebClient();
            try
            {
                var result = "";
                foreach (var item in @params)
                {
                    result += item.Key + "=" + item.Value + "&";
                }

                var path = _url + $"/api.php?{result}";
                var json = await wc.DownloadStringTaskAsync(path);

                var data = JSONNode.Parse(json);

                if (data is null)
                    throw new NullReferenceException("JSON string is null");

                callback?.Invoke(data);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        public void Upload(string key, object data, Action<bool> callback = null)
        {
            Debug.Log("Saved to the server..");
            //throw new NotImplementedException();
        }
    }
}