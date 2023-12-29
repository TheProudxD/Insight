using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Shop
{
    [RequireComponent(typeof(Image))]
    public class ShopItemView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<ShopItemView> Click;

        [SerializeField] private Color _standardBackground;
        [SerializeField] private Color _highlightBackground;
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _selectionText;
        [SerializeField] private IntValueView _priceView;

        public ShopItem Item { get; private set; }
        public int Price => Item.Price;
        public bool IsLock { get; private set; }
        public Sprite Model => Item.Model;
        private Image _backgroundImage;

        public void Initialize(ShopItem shopItem)
        {
            Item = shopItem;
            _backgroundImage = GetComponent<Image>();
            _backgroundImage.color = _standardBackground;
            _contentImage.sprite = shopItem.Sprite;

            _priceView.Show(Price);
        }

        public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

        public void Lock()
        {
            IsLock = true;
            _lockImage.gameObject.SetActive(true);
            _priceView.Show(Price);
        }

        public void Unlock()
        {
            IsLock = false;
            _lockImage.gameObject.SetActive(false);
            _priceView.Hide();
        }

        public void Select() => _selectionText.gameObject.SetActive(true);
        public void Unselect() => _selectionText.gameObject.SetActive(false);

        public void Highlight() => _backgroundImage.color = _highlightBackground;
        public void UnHighlight() => _backgroundImage.color = _standardBackground;
    }
}