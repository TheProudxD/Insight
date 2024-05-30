using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class CommonWindow : MonoBehaviour
    {
        [SerializeField] protected Button CloseButton;
        private readonly List<GameObject> _children = new();

        protected virtual void OnEnable() => CloseButton.onClick.AddListener(Close);
        
        protected virtual void OnDisable() => CloseButton.onClick.RemoveListener(Close);

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