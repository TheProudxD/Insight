using System.Collections.Generic;


namespace Extensions
{
    public static class HashSetExtensions
    {
        public static bool IsNullOrEmpty<T>(this HashSet<T> set) => set == null || set.Count == 0;

        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                set.Add(item);
            }
        }

        public static T ItemAtIndexLinear<T>(this HashSet<T> set, int index)
        {
            if (set.IsNullOrEmpty() || index >= set.Count)
            {
                return default(T);
            }

            T result = default(T);
            int seeker = 0;
            set.ForEach(item =>
            {
                if (seeker == index)
                {
                    result = item;
                    return true;
                }

                seeker++;
                return false;
            });

            return result;
        }
    }
}
