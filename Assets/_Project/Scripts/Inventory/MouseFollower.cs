using System;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class MouseFollower : MonoBehaviour
    {
        private Canvas _canvas;
        private Camera _camera;

        private void Awake()
        {
            _canvas = FindObjectOfType<Canvas>();
            _camera = FindObjectOfType<Camera>();
        }

        public void Follow()
        {
            print("MouseFollower");

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_canvas.transform, Input.mousePosition, _canvas.worldCamera, out var point);

            transform.position = _canvas.transform.TransformPoint(point);
        }

        public void Toggle(bool val) => enabled = val;
    }
}