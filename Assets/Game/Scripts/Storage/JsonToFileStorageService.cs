using System;
using System.IO;
using UnityEngine;

namespace Game.Scripts.Storage
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
        
        private string BuildPath(string key) => Path.Combine(Application.persistentDataPath, key);
    }
}