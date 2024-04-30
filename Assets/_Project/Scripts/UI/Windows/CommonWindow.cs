using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class CommonWindow : MonoBehaviour
    {
        private readonly List<GameObject> _children = new();

        public virtual void Close()
        {
            _children.ForEach(x => x.SetActive(false));
        }

        public virtual void Show()
        {
            var childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                _children.Add(transform.GetChild(i).gameObject);
            }
            _children.ForEach(x => x.SetActive(true));
        }
    }
}