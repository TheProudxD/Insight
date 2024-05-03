using System.Collections.Generic;


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

        public static T ReturnRandom<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
    }
}
