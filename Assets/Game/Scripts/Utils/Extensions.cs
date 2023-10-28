using System.IO;
using UnityEngine;

namespace Utils
{
    public static class Extensions
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
    }
}