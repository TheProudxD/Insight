using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace StorageService
{
    public sealed class ServerJSONStorageService : IStaticStorageService
    {
        private readonly string _url;

        public ServerJSONStorageService(string url) => _url = url;

        public async void Upload(string key, object data, Action<bool> callback = null)
        {
            try
            {
                using var wc = new WebClient();
                var path = Utils.BuildPath(key);
                var iri = new Uri(_url + "/" + key);
                await wc.UploadFileTaskAsync(iri, path);
                callback?.Invoke(true);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        public async Task Download(string key, Action<StaticData> callback)
        {
            try
            {
                using var wc = new WebClient();    
                
                var serverJsonFile = await wc.DownloadStringTaskAsync(_url + "/" + key);
                var serverData = JsonUtility.FromJson<StaticData>(serverJsonFile);

                if (Utils.ExistLocalDataJSON())
                {
                    var localPath = Utils.BuildPath(DataManager.STATIC_DATA_KEY);
                    var localJsonFile = File.ReadAllText(localPath);
                    var localData = JsonUtility.FromJson<StaticData>(localJsonFile);
                    if (localData.GetHashCode() != serverData.GetHashCode())
                    {
                        File.WriteAllText(localPath, serverJsonFile);
                    }
                    callback?.Invoke(serverData);
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }
    }
}