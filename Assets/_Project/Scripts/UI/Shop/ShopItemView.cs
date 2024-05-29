using System;
using ResourceService;
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
        [SerializeField] private GameObject _selectionText;
        [SerializeField] private IntValueView _priceView;
        [SerializeField] private Image _currencyImage;
        [SerializeField] private Sprite _softCurrencyIcon;
        [SerializeField] private Sprite _hardCurrencyIcon;

        public ShopItem Item { get; private set; }
        public bool IsLock { get; private set; }

        private Image _backgroundImage;

        public void Initialize(ShopItem shopItem)
        {
            Item = shopItem;
            _backgroundImage = GetComponent<Image>();
            _backgroundImage.color = _standardBackground;
            _contentImage.sprite = shopItem.Sprite;
            _priceView.Show(Item.Price);

            _currencyImage.sprite = Item.ResourceType switch
            {
                ResourceType.SoftCurrency => _softCurrencyIcon,
                ResourceType.HardCurrency => _hardCurrencyIcon,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

        public void Lock()
        {
            IsLock = true;
            _lockImage.gameObject.SetActive(true);
            _priceView.Show(Item.Price);
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