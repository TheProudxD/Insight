using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileExtensions
{
    public static string BuildPath(string key)
    {
        var path = Path.Combine(Application.persistentDataPath, key);
        if (!File.Exists(path))
        {
            using (var file = File.Create(path)) ;
        }
        return path;
    }
}
