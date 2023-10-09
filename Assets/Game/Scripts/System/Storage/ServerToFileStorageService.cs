using StorageService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class ServerJsonToFileStorageService : IStorageService
{
    private readonly string _url;
    public ServerJsonToFileStorageService(string url)
    {
        Debug.Log(url);
        this._url = url;
    }

    public async Task Load<T>(string key, Action<T> callback)
    {
        try
        {
            using (WebClient wc = new WebClient())
            {
                var path = FileExtensions.BuildPath(key);
                var jsonFile = await wc.DownloadStringTaskAsync(_url + @"/" + key);
                var data = JsonUtility.FromJson<T>(jsonFile);
                Debug.Log(data.ToString());
                callback(data);
            }
        }
        catch(Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }

    public async void Save(string key, object data, Action<bool> callback = null)
    {
        try
        {
            using (WebClient wc = new WebClient())
            {
                var path = FileExtensions.BuildPath(key);
                await wc.UploadFileTaskAsync (
                   // Param1 = Link of file
                   new System.Uri(_url + @"/" + key),
                   // Param2 = Path to save
                   path
               );
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }
    }
}
