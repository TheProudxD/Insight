using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepCopy<T>(this T self)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("Type must be iserializable");
            }

            if (ReferenceEquals(self, null))
                return default;

            var formatter = new BinaryFormatter();
            using var stream = new MemoryStream();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }

        public static T With<T>(this T self, Action<T> set)
        {
            set?.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if (when())
                apply?.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> set, bool when)
        {
            if (when)
                set?.Invoke(self);
            return self;
        }
    }
}