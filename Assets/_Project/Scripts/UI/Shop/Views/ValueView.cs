using System;
using TMPro;
using UnityEngine;

namespace UI.Shop
{
    public class ValueView<T> : MonoBehaviour where T : IConvertible
    {
        [SerializeField] private TMP_Text _text;

        public void Show(T value)
        {
            gameObject.SetActive(true);
            _text.SetText(value.ToString());
        }

        public void Hide() => gameObject.SetActive(false);
    }
}