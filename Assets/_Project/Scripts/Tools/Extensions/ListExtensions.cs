using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T ReturnRandom<T>(this List<T> list, List<T> itemsToExclude)
        {
            var val = list[UnityEngine.Random.Range(0, list.Count)];

            while (itemsToExclude.Contains(val))
                val = list[UnityEngine.Random.Range(0, list.Count)];

            return val;
        }

        public static bool IsEmpty<T>(this List<T> list) => list.Count == 0;
        
        public static T ReturnRandom<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];

        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = list.Count - 1; i > 1; i--)
            {
                var j = Random.Range(0, i + 1);
                (list[j], list[i]) = (list[i], list[j]);
            }
        }
        
        public static void Off(this List<GameObject> list)
        {
            foreach (var obj in list)
                obj.SetActive(false);
        }

        public static void On(this List<GameObject> list)
        {
            foreach (var obj in list)
                obj.SetActive(true);
        }

        public static int GetEqualsCount<T>(this List<T> list, T obj)
        {
            int index = 0;

            foreach (var item in list)
                if (item.Equals(obj))
                    index++;

            if (index > 0)
                return index;
            else return -1;
        }

        public static void All<T>(this List<T> list, Action<T> action)
        {
            foreach (var item in list)
                action(item);
        }
    }
}