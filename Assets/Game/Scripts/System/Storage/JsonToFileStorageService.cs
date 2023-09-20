using System;
using System.IO;
using UnityEngine;

namespace StorageService
{
    public class JsonToFileStorageService: IStorageService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            string json = JsonUtility.ToJson(data);
            using var fileStream = new StreamWriter(path);
            fileStream.Write(json);
            callback?.Invoke(true);
        }
        
        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);
            using var fileStream = new StreamReader(path);
            var json = fileStream.ReadToEnd();
            var data = JsonUtility.FromJson<T>(json);
                
            callback?.Invoke(data);
        }

        private string BuildPath(string key) {
            var path = Path.Combine(Application.persistentDataPath, key);
            if (!File.Exists(path))
            {
                File.Create(path, 10);
            }
            return path;
        }
    }
}