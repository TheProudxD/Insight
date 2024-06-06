using Storage.Static;
using StorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.Shop.Data
{
    public class ShopData
    {
        private const string SAVE_NEW_SHOP_ITEM_KEY = "changeitems";
        private const string GET_SHOP_ITEMS_KEY = "getitems";

        private readonly ShopContent _shopContent;
        private readonly List<SwordSkins> _openedSwordSkins;
        private readonly List<BowSkins> _openedBowSkins;

        private BowSkins _selectedBowSkins;
        private SwordSkins _selectedSwordSkins;
        private IDynamicStorageService DynamicStorageService { get; }

        public IEnumerable<BowSkins> OpenedBowSkins => _openedBowSkins;
        public IEnumerable<SwordSkins> OpenSwordSkins => _openedSwordSkins;

        public BowSkins SelectedBowSkins
        {
            get => _selectedBowSkins;
            set
            {
                if (_openedBowSkins.Contains(value) == false)
                    throw new ArgumentException();

                _selectedBowSkins = value;
            }
        }

        public SwordSkins SelectedSwordSkins
        {
            get => _selectedSwordSkins;
            set
            {
                if (_openedSwordSkins.Contains(value) == false)
                    throw new ArgumentException();
                _selectedSwordSkins = value;
            }
        }

        public ShopData(IDynamicStorageService dynamicStorageService, ShopContent shopContent)
        {
            DynamicStorageService = dynamicStorageService;
            _shopContent = shopContent;

            _selectedSwordSkins = _shopContent.SwordSkinItems.First().SkinType;
            _selectedBowSkins = _shopContent.BowSkinItems.First().SkinType;

            _openedBowSkins = new List<BowSkins> { _selectedBowSkins };
            _openedSwordSkins = new List<SwordSkins> { _selectedSwordSkins };
        }

        public void OpenSwordSkin(SwordSkins item)
        {
            if (_openedSwordSkins.Contains(item))
                throw new ArgumentException(nameof(item));

            _selectedSwordSkins = item;
            _openedSwordSkins.Add(item);
            SaveNewItem((int)item);
        }

        public void OpenBowSkin(BowSkins item)
        {
            if (_openedBowSkins.Contains(item))
                throw new ArgumentException(nameof(item));

            _selectedBowSkins = item;
            _openedBowSkins.Add(item);
            SaveNewItem((int)item);
        }

        private async void SaveNewItem(int id, int amount = 1)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "itemid", id.ToString() },
                { "itemamount", amount.ToString() },
                { "action", SAVE_NEW_SHOP_ITEM_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await DynamicStorageService.Upload(uploadParams,
                result =>
                {
                    Debug.Log(result
                        ? $"New shop item saved successfully with id - {id}"
                        : "Error while saving shop item");
                });
        }

        public async Task GetBoughtItems()
        {
            var @params = new Dictionary<string, string>
            {
                { "action", GET_SHOP_ITEMS_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };
            var data = await DynamicStorageService.Download(@params);
            var allItems = UnionAllShopItems();

            for (int id = 0; id < allItems.Count; id++)
            {
                var item = allItems[id];
                var amount = data[id];

                if (amount > 0)
                {
                    switch (item)
                    {
                        case SwordSkinItem sword:
                            _openedSwordSkins.Add(sword.SkinType);
                            break;
                        case BowSkinItem bow:
                            _openedBowSkins.Add(bow.SkinType);
                            break;
                    }
                }
            }
        }

        private List<ShopItem> UnionAllShopItems()
        {
            var allItems = new List<ShopItem>();
            allItems.AddRange(_shopContent.SwordSkinItems.OrderBy(x => x.ID));
            allItems.AddRange(_shopContent.BowSkinItems.OrderBy(x => x.ID));
            return allItems;
        }
    }
}