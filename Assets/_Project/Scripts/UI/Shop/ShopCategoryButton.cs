using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopCategoryButton : MonoBehaviour
    {
        public Action Click;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _categoryTitle;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;
        [SerializeField] private Image _focus;

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        public void Select()
        {
            _categoryTitle.color = _selectedColor;
            _focus.transform.position = new Vector2(_button.transform.position.x, _focus.transform.position.y);
        }

        public void Unselect() => _categoryTitle.color = _unselectedColor;

        private void OnClick() => Click?.Invoke();
    }
}