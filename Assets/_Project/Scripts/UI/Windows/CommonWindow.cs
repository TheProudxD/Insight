using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public abstract class CommonWindow : MonoBehaviour
    {
        [Inject] protected UIAudioPlayer AudioPlayer;
        [SerializeField] protected Button CloseButton;
        private readonly List<GameObject> _children = new();

        protected virtual void OnEnable()
        {
            if (this is DialogBox)
                return;

            if (CloseButton != null)
            {
                CloseButton.onClick.AddListener(Close);
                CloseButton.onClick.AddListener(AudioPlayer.PlayButtonSound);
            }
            else
            {
                Debug.Log($"{nameof(CloseButton)} is null on {gameObject}");
            }
        }

        protected virtual void OnDisable()
        {
            if (this is DialogBox)
                return;

            if (CloseButton != null)
            {
                CloseButton.onClick.RemoveListener(Close);
                CloseButton.onClick.RemoveListener(AudioPlayer.PlayButtonSound);
            }
            else
            {
                Debug.Log($"{nameof(CloseButton)} is null on {gameObject}");
            }
        }

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