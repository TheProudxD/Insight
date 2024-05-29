using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Extensions
{
    public static class ArrayExtensions
    {
        public static T RandomObject<T>(this IList<T> list) =>
            list.Count > 0 ? list[Random.Range(0, list.Count)] : default;

        public static List<T> RandomObjects<T>(this IEnumerable<T> list, int elementsCount)
        {
            var enumerable = list.ToList();
            elementsCount = Mathf.Clamp(elementsCount, 1, enumerable.Count());

            return enumerable.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
        }

        public static T LastObject<T>(this IList<T> list) => list.Count > 0 ? list[^1] : default;

        public static T FirstObject<T>(this IList<T> list) => list.Count > 0 ? list[0] : default;

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            var result = source;

            if (index < 0 || index >= source.Length)
                return result;
            result = new T[source.Length - 1];

            if (index > 0)
            {
                Array.Copy(source, 0, result, 0, index);
            }

            if (index < source.Length - 1)
            {
                Array.Copy(source, index + 1, result, index, source.Length - index - 1);
            }

            return result;
        }

        public static List<T> Diff<T>(IList<T> array1, IList<T> array2)
        {
            var subset = array1.Where(e1 => !array2.Contains(e1)).ToList();
            subset.AddRange(array2.Where(e2 => !array1.Contains(e2)));

            return subset;
        }

        public static bool ArrayEqual<T>(this IList<T> array1, IList<T> array2) where T : Object
        {
            bool result;

            if (array2 == null || array1 == null)
            {
                result = false;
            }
            else
            {
                result = array1.Count == array2.Count;

                if (result == false) 
                    return false;
                
                for (int i = 0; i < array1.Count && result; i++)
                {
                    if (array1[i])
                    {
                        result &= array1[i] == array2[i];
                    }
                }
            }

            return result;
        }

        public static T[] EnsureLength<T>(this T[] array, int desiredLength, bool ensureAtLeast = false)
        {
            if (array == null ||
                (ensureAtLeast ? array.Length < desiredLength : array.Length != desiredLength))
            {
                return new T[desiredLength];
            }

            return array;
        }

        public static bool ArrayContains<T>(this T[] array, T value)
        {
            var result = array is not null && value is not null;
            return result && array.Contains(value);
        }

        public static L InitWith<L, T>(this L list, T with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                list.Add(with);
            }

            return list;
        }

        public static L InitWith<L, T>(this L list, Func<T> with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                list.Add(with());
            }

            return list;
        }

        public static L InitWith<L, T>(this L list, Func<int, T> with, int count) where L : IList<T>
        {
            list.Clear();

            for (int i = 0; i < count; i++)
            {
                list.Add(with(i));
            }

            return list;
        }

        public static T[] FillWith<T>(this T[] array, T with)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = with;
            }

            return array;
        }

        public static T[] FillWith<T>(this T[] array, Func<T> with)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = with();
            }

            return array;
        }

        public static T[] FillWith<T>(this T[] array, Func<int, T> with)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = with(i);
            }

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, T with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    array[i, j] = with;
                }
            }

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, Func<T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    array[i, j] = with();
                }
            }

            return array;
        }

        public static T[,] FillWith<T>(this T[,] array, Func<int, int, T> with)
        {
            int w = array.GetLength(0);
            int h = array.GetLength(1);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    array[i, j] = with(i, j);
                }
            }

            return array;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        /// <summary>
        /// Add element if it isn't added yet
        /// </summary>
        /// <returns>Indicates added element or not</returns>
        public static bool AddExclusive<T>(this IList<T> list, T element)
        {
            if (list.Contains(element))
                return false;

            list.Add(element);

            return true;
        }

        /// <summary>
        /// Good for tiny Lists instead of Distinct()
        /// </summary>
        public static void AddRangeExclusive<T>(this IList<T> list, IList<T> elements)
        {
            foreach (var t in elements)
            {
                list.AddExclusive(t);
            }
        }

        /// <summary>
        /// Add element if it isn't added yet
        /// </summary>
        /// <returns>Indicates added element or not</returns>
        public static bool InsertExclusive<T>(this IList<T> list, int index, T element)
        {
            if (list.Contains(element)) return false;

            index = index.Clamp(0, list.Count);
            if (index == list.Count)
            {
                list.Add(element);
            }
            else
            {
                list.Insert(index, element);
            }

            return true;
        }

        /// <summary>
        /// Return 'true' in func to break the cycle
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> list, Func<T, bool> breakFunc)
        {
            if (breakFunc == null)
            {
                return;
            }

            foreach (var _ in list.Where(breakFunc))
            {
                break;
            }
        }

        public static bool IsOneOf<T>(this T self, params T[] elem) => elem.Contains(self);

        public static T[] Concat<T>(this T[] x, T[] y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));
            var oldLen = x.Length;
            Array.Resize(ref x, x.Length + y.Length);
            Array.Copy(y, 0, x, oldLen, y.Length);
            return x;
        }

        public static int ReturnNearestIndex(this Vector3[] nodes, Vector3 destination)
        {
            var nearestDistance = Mathf.Infinity;
            var index = 0;
            var length = nodes.Length;
            for (var i = 0; i < length; i++)
            {
                var distanceToNode = (destination + nodes[i]).sqrMagnitude;
                if (!(nearestDistance > distanceToNode)) continue;
                nearestDistance = distanceToNode;
                index = i;
            }

            return index;
        }

        public static float[] CumulateValues(this float[] array, bool doProgressively = true)
        {
            if (doProgressively)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i > 0 ? array[i] + array[i - 1] : array[i];
                }
            }
            else
            {
                for (int i = array.Length - 1; i >= 0; i++)
                {
                    array[i] = i == array.Length - 1 ? array[i] : array[i] + array[i + 1];
                }
            }

            return array;
        }

        public static T FirstWhich<T>(this T[] array, Predicate<T> predicate)
        {
            if (array == null) throw new NullReferenceException("Array is null");
            if (array.Length == 0) throw new IndexOutOfRangeException("Array is empty");
            foreach (var t in array.Where(t => predicate(t)))
            {
                return t;
            }

            throw new ArgumentException("Cant find predicate in array");
        }
    }
}