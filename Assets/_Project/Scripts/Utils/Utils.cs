using StorageService;
using System.IO;
using UnityEngine;

namespace Tools
{
    public static class Utils
    {
        public static string BuildPath(string key)
        {
            var path = Path.Combine(Application.persistentDataPath, key); // C:\Users\<user>\AppData\LocalLow\<company name>\Insight\key
            if (!File.Exists(path))
            {
                using var file = File.Create(path);
            }

            return path;
        }
        
        public static bool ExistLocalDataJSON()
        {
            var path = Path.Combine(Application.persistentDataPath, DataManager.STATIC_DATA_KEY); // C:\Users\<user>\AppData\LocalLow\<company name>\Insight\key
            return File.Exists(path);
        } 
        
        public static bool ExistSystemDataJSON()
        {
            var path = Path.Combine(Application.persistentDataPath, DataManager.SYSTEM_DATA_KEY); // C:\Users\<user>\AppData\LocalLow\<company name>\Insight\System
            return File.Exists(path);
        }

        public static string GiveAllFields<T>(this T obj)
        {
            var str = "";
            foreach (var field in typeof(T).GetFields())
                str += field.Name+ ": " + field.GetValue(obj)+ "/";
            return str;
        }
        
        public static void Print(string str) => Debug.Log(str);
    }
}