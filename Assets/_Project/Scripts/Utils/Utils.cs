using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Tools
{
    public static class Utils
    {
        public static string BuildPath(string key)
        {
            // C:\Users\<user>\AppData\LocalLow\<company name>\Insight\key
            var path = Path.Combine(Application.persistentDataPath, key); 

            if (!File.Exists(path))
            {
                using var file = File.Create(path);
            }

            return path;
        }

        public static string GiveAllFields<T>(this T obj)
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance).Aggregate("",
                (current, field) => current + field.Name + ": " + field.GetValue(obj) + " ");
        }

        public static void Print(string str) => Debug.Log(str);
    }
}