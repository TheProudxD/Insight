using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static void Activate(this GameObject component)
        {
            if (component) component.gameObject.SetActive(true);
        }

        public static void Deactivate(this GameObject component)
        {
            if (component) component.gameObject.SetActive(false);
        }

        public static void Activate(this Component component)
        {
            if (component) component.gameObject.SetActive(true);
        }

        public static void Deactivate(this Component component)
        {
            if (component) component.gameObject.SetActive(false);
        }

        public static T GetOrAddComponent<T>(this Component child) where T : Component =>
            child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
    }
}