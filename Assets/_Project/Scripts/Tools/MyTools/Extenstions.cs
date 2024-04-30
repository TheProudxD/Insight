using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    }

    public static class ButtonExtensions
    {
        public static void Add(this Button button, UnityAction action) => button.onClick.AddListener(action);
        public static void Remove(this Button button, UnityAction action) => button.onClick.RemoveListener(action);
        public static void RemoveAll(this Button button) => button.onClick.RemoveAllListeners();
    }
}