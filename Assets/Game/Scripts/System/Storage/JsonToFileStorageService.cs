using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace StorageService
{
    public class JsonToFileStorageService: IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = FileExtensions. BuildPath(key);
            string json = JsonUtility.ToJson(data);
            using var fileStream = new StreamWriter(path);
            fileStream.Write(json);
            callback?.Invoke(true);
        }
        
        public async Task Load<T>(string key, Action<T> callback)
        {
            string path = FileExtensions.BuildPath(key);
            using var fileStream = new StreamReader(path);
            var json = fileStream.ReadToEnd();
            var data = JsonUtility.FromJson<T>(json);
            callback?.Invoke(data);
        }
    }
}