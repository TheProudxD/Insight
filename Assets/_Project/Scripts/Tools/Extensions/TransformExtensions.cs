using System.Collections.Generic;
using UnityEngine;


namespace Extensions
{
    public static class TransformExtensions
    {
        public static Transform FindDeep(this Transform obj, string name)
        {
            if (obj.name == name)
            {
                return obj;
            }

            var count = obj.childCount;
            for (var i = 0; i < count; ++i)
            {
                var posObj = obj.GetChild(i).FindDeep(name);
                if (posObj != null)
                {
                    return posObj;
                }
            }

            return null;
        }

        public static bool TryFind(this Transform obj, string name, out Transform foundObj)
        {
            foundObj = default;

            var foundedObject = obj.Find(name);

            foundObj = foundedObject != null
                ? foundedObject
                : throw new System.NullReferenceException("Can't find transform of object: " +
                                                          obj.name + " with name: " + name);
            return true;
        }

        public static bool TryFindDeep(this Transform obj, string name, out Transform foundObj)
        {
            foundObj = default;

            var foundedObject = obj.FindDeep(name);

            if (foundedObject == null)
                throw new System.NullReferenceException("Can't find transform of object: " +
                                                        obj.name + " with name: " + name);
            foundObj = foundedObject;
            return true;
        }

        public static List<T> GetAll<T>(this Transform obj)
        {
            var results = new List<T>();
            obj.GetComponentsInChildren(results);
            return results;
        }

        public static void MoveUp(this Transform obj, float value) => obj.position += Vector3.up * value;
        
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
        }

        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}