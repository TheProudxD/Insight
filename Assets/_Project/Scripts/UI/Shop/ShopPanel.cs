using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Shop
{
    public class ShopPanel : MonoBehaviour
    {
        public event Action<ShopItemView> ItemViewClicked;

        [SerializeField] private Transform _itemParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

        private List<ShopItemView> _shopItems = new();
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
            
            foreach (var spawnedItem in items.Select(shopItem => _shopItemViewFactory.Get(shopItem, _itemParent)))
            {
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
            
            Sort();
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

        public void Select(ShopItemView shopItem)
        {
            foreach (var item in _shopItems)
            {
                item.Unselect();
            }
            
            shopItem.Select();
        }

        private void Sort()
        {
            _shopItems = _shopItems.OrderBy(x => x.IsLock).ThenByDescending(x => x.Item.Price).ToList();
            
            for (var i = 0; i < _shopItems.Count; i++) 
                _shopItems[i].transform.SetSiblingIndex(i);
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