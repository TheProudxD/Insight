using StorageService;
using System;
using Utils;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public sealed class ServerJSONStorageService : IStorageService
{
    private readonly string _url;

    public ServerJSONStorageService(string url) => _url = url;
    
    public async void Save(string key, object data, Action<bool> callback = null)
    {
        try
        {
            using var wc = new WebClient();
            var path = Extensions.BuildPath(key);
            var iri = new Uri(_url + "/" + key);
            await wc.UploadFileTaskAsync(iri, path);
            callback?.Invoke(true);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }

    public async Task Load(string key, Action<JSONData> callback)
    {
        try
        {
            using var wc = new WebClient();
            var jsonFile = await wc.DownloadStringTaskAsync(_url + "/" + key);
            var data = JsonUtility.FromJson<JSONData>(jsonFile);
            callback?.Invoke(data);
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }
}