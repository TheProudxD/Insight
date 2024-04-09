using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UI.Shop;
using UnityEngine;

namespace Tools
{
    public static class InsightUtils
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
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Aggregate("", (current, field) => current + field.Name + ": " + field.GetValue(obj) + " ");
        }

        public static void IsCorrectShopItemsId()
        {
            var swordMax = Enum.GetValues(typeof(SwordSkins)).Cast<int>();
            var bowMin = Enum.GetValues(typeof(BowSkins)).Cast<int>();
            var errorsCount = bowMin.Intersect(swordMax).Count();
            if (errorsCount != 0)
                Debug.LogError($"something wrong with shop items Id!. There are {errorsCount} errors.");
        }
        
        public static bool IsItPlayer(Collider2D collision) => 
            collision.CompareTag(Constants.PLAYER_TAG) && !collision.isTrigger;
    }
}