using System;
using System.IO;
using Tools;
using System.Threading.Tasks;
using UnityEngine;

namespace StorageService
{
    public sealed class LocalJSONStorageService : IStaticStorageService
    {
        public void Upload(string key, object data, Action<bool> callback = null)
        {
            var path = InsightUtils.BuildPath(key);
            var json = JsonUtility.ToJson(data);
            using var fileStream = new StreamWriter(path);
            fileStream.Write(json);
            callback?.Invoke(true);
        }

        public Task Download(string key, Action<GameData> callback)
        {
            var path = InsightUtils.BuildPath(key);
            using var fileStream = new StreamReader(path);
            var json = fileStream.ReadToEnd();
            var data = JsonUtility.FromJson<GameData>(json);
            callback?.Invoke(data);
            return Task.CompletedTask;
        }
    }
}