using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandomItem<T>(this IList<T> list) =>
        list is { Count: > 0 } ? list[Random.Range(0, list.Count)] : default;

    public static void Shuffle<T>(this IList<T> list)
    {
        for (var i = list.Count - 1; i > 1; i--)
        {
            var j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}