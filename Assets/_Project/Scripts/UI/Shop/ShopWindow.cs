using Managers;
using ResourceService;
using UI.Shop.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Shop
{
    public class ShopWindow : CommonWindow
    {
        [SerializeField] private ShopContent _shopContent;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private ShopCategoryButton _swordCategoryButton;
        [SerializeField] private ShopCategoryButton _bowCategoryButton;
        [SerializeField] private SkinPlacement _skinPlacement;

        [Header("Buttons")] [SerializeField] private BuyButton _buyButton;
        [SerializeField] private Button _selectionButton;
        [Header("Text")] [SerializeField] private Image _selectedText;

        [Inject] private WindowManager _windowManager;
        [Inject] private ResourceManager _resourceManager;
        [Inject] private ShopData _shopData;
        [Inject] private SkinSelector _skinSelector;
        [Inject] private SkinUnlocker _skinUnlocker;
        [Inject] private OpenedSkinsChecker _openedSkinsChecker;
        [Inject] private SelectedSkinsChecker _selectedSkinsChecker;

        private ShopItemView _previewedItem;

        public override async void Show()
        {
            base.Show();

            await _shopData.GetBoughtItems();
            _shopPanel.Initialize(_openedSkinsChecker, _selectedSkinsChecker);
            _shopPanel.ItemViewClicked += OnItemViewClicked;
            OnSwordSkinButtonClick();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _swordCategoryButton.Click += OnSwordSkinButtonClick;
            _bowCategoryButton.Click += OnBowSkinButtonClick;
            _buyButton.Click += OnBuyButtonClicked;
            _selectionButton.onClick.AddListener(OnSelectButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _swordCategoryButton.Click -= OnSwordSkinButtonClick;
            _bowCategoryButton.Click -= OnBowSkinButtonClick;
            _shopPanel.ItemViewClicked -= OnItemViewClicked;
            _buyButton.Click -= OnBuyButtonClicked;
            _selectionButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        private void OnSwordSkinButtonClick()
        {
            _swordCategoryButton.Select();
            _bowCategoryButton.Unselect();
            _shopPanel.Show(_shopContent.SwordSkinItems);
        }

        private void OnBowSkinButtonClick()
        {
            _bowCategoryButton.Select();
            _swordCategoryButton.Unselect();
            _shopPanel.Show(_shopContent.BowSkinItems);
        }

        private void ShowSelectionButton()
        {
            _selectionButton.gameObject.SetActive(true);
            HideBuyButton();
            HideSelectedText();
        }

        private void ShowSelectedText()
        {
            _selectedText.gameObject.SetActive(true);
            HideSelectionButton();
            HideBuyButton();
        }

        private void OnBuyButtonClicked()
        {
            var rt = _previewedItem.Item.ResourceType;
            var price = _previewedItem.Item.Price;
            if (_resourceManager.IsEnough(rt, price))
            {
                _resourceManager.Spend(rt, price);
                _skinUnlocker.Visit(_previewedItem.Item);
                SelectSkin();
                _previewedItem.Unlock();
            }
        }

        private void OnSelectButtonClicked()
        {
            SelectSkin();
        }

        private void OnItemViewClicked(ShopItemView shopItemView)
        {
            _previewedItem = shopItemView;
            var model = _previewedItem.Item.Model;
            var itemName = _previewedItem.Item.Name;
            var description = _previewedItem.Item.Description;
            var rt = _previewedItem.Item.ResourceType;
            var price = _previewedItem.Item.Price;

            _openedSkinsChecker.Visit(_previewedItem.Item);
            _skinPlacement.SetGameModel(model, itemName, description);

            if (_openedSkinsChecker.IsOpened)
            {
                _selectedSkinsChecker.Visit(_previewedItem.Item);
                if (_selectedSkinsChecker.IsSelected)
                {
                    ShowSelectedText();
                    return;
                }

                ShowSelectionButton();
            }
            else
            {
                ShowBuyButton(rt, price);
            }
        }

        private void ShowBuyButton(ResourceType resourceType, int price)
        {
            _buyButton.gameObject.SetActive(true);
            _buyButton.UpdateText(price);
            _buyButton.SetResourceType(resourceType);

            if (_resourceManager.IsEnough(resourceType, price))
            {
                _buyButton.Unlock();
            }
            else
            {
                _buyButton.Lock();
            }

            HideSelectedText();
            HideSelectionButton();
        }

        private void SelectSkin()
        {
            _skinSelector.Visit(_previewedItem.Item);
            _shopPanel.Select(_previewedItem);
            ShowSelectedText();
        }

        private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
        private void HideSelectedText() => _selectedText.gameObject.SetActive(false);
        private void HideBuyButton() => _buyButton.gameObject.SetActive(false);

        private void CloseWindow() => _windowManager.CloseShopWindow();
    }
}