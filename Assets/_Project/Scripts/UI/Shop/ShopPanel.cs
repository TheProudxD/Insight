using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Shop
{
    public class ShopPanel : MonoBehaviour
    {
        public event Action<ShopItemView> ItemViewClicked;

        [SerializeField] private Transform _itemParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

        private readonly List<ShopItemView> _shopItems = new();
        private OpenedSkinsChecker _openedSkinsChecker;
        private SelectedSkinsChecker _selectedSkinsChecker;

        public void Initialize(OpenedSkinsChecker openedSkinsChecker, SelectedSkinsChecker selectedSkinsChecker)
        {
            _openedSkinsChecker = openedSkinsChecker;
            _selectedSkinsChecker = selectedSkinsChecker;
        }

        public void Show(IEnumerable<ShopItem> items)
        {
            Clear();
            foreach (var shopItem in items)
            {
                var spawnedItem = _shopItemViewFactory.Get(shopItem, _itemParent);
                spawnedItem.Click += OnItemViewClick;

                spawnedItem.Unselect();
                spawnedItem.UnHighlight();

                _openedSkinsChecker.Visit(spawnedItem.Item);
                if (_openedSkinsChecker.IsOpened)
                {
                    _selectedSkinsChecker.Visit(spawnedItem.Item);

                    if (_selectedSkinsChecker.IsSelected)
                    {
                        spawnedItem.Select();
                        spawnedItem.Highlight();
                        ItemViewClicked?.Invoke(spawnedItem);
                    }

                    spawnedItem.Unlock();
                }
                else
                {
                    spawnedItem.Lock();
                }

                _shopItems.Add(spawnedItem);
            }
        }

        private void OnItemViewClick(ShopItemView shopItemView)
        {
            Highlight(shopItemView);
            ItemViewClicked?.Invoke(shopItemView);
        }

        private void Highlight(ShopItemView shopItemView)
        {
            foreach (var item in _shopItems)
                item.UnHighlight();

            shopItemView.Highlight();
        }

        private void Clear()
        {
            foreach (var shopItem in _shopItems)
            {
                shopItem.Click -= OnItemViewClick;
                Destroy(shopItem.gameObject);
            }

            _shopItems.Clear();
        }
    }
}