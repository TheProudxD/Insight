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

        public async Task Download(string key, Action<GameData> callback)
        {
            try
            {
                using var wc = new WebClient();

                var serverJsonFile = await wc.DownloadStringTaskAsync(_url + "/" + key);
                var serverData = JsonUtility.FromJson<GameData>(serverJsonFile);
                var localPath = Utils.BuildPath(DataManager.MAX_LEVEL_DATA_KEY);

                if (!File.Exists(localPath))
                {
                    var localJsonFile = await File.ReadAllTextAsync(localPath);
                    var localData = JsonUtility.FromJson<GameData>(localJsonFile);
                    if (localData.GetHashCode() != serverData.GetHashCode())
                    {
                        await File.WriteAllTextAsync(localPath, serverJsonFile);
                    }
                }
                else if (File.Exists(localPath))
                {
                    await File.WriteAllTextAsync(localPath, serverJsonFile);
                }

                callback?.Invoke(serverData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }
    }
}