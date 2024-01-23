using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Zenject;

namespace UI
{
    public class WindowCommon : MonoBehaviour
    {
        private readonly List<Transform> _children = new();

        private void Awake()
        {
            var thisTransform = gameObject.transform;
            var childCount = thisTransform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                _children.Add(thisTransform.GetChild(i));
            }
        }

        public void Close()
        {
            _children.ForEach(x => x.gameObject.SetActive(false));
        }

        public void Show()
        {
            _children.ForEach(x => x.gameObject.SetActive(true));
        }
    }
}