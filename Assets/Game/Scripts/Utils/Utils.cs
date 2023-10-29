using System.IO;
using UnityEngine;

namespace Tools
{
    public static class Utils
    {
        public static string BuildPath(string key)
        {
            var path = Path.Combine(Application.persistentDataPath, key);
            if (!File.Exists(path))
            {
                using var file = File.Create(path);
            }

            return path;
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