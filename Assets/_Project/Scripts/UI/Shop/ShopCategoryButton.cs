using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopCategoryButton : MonoBehaviour
    {
        public Action Click;

        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        public void Select() => _image.color = _selectedColor;
        public void Unselect() => _image.color = _unselectedColor;

        private void OnClick() => Click?.Invoke();
    }
}