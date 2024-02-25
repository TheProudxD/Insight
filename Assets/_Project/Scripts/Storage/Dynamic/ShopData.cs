using Assets._Project.Scripts.Storage.Static;
using SimpleJSON;
using StorageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UI.Shop.Data
{
    public class ShopData
    {
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
            //SaveNewItem(item);
        }

        public void OpenBowSkin(BowSkins item)
        {
            if (_openedBowSkins.Contains(item))
                throw new ArgumentException(nameof(item));

            _selectedBowSkins = item;
            _openedBowSkins.Add(item);
            //SaveNewItem(item);
        }

        private async void SaveNewItem(ShopItem skin)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "itemid", skin.ID.ToString() },
                { "itemamount", "1" },
                { "action", "changeitems" },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await DynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    Debug.Log($"New shop item saved successfully - {skin.name}");
                }
                else
                {
                    Debug.Log("Error while saving shop item");
                }
            });
        }

        public async Task GetBoughtItems()
        {
            using var wc = new WebClient();
            var remotePath =
                $"http://game.ispu.ru/insight/api.php?action=getitems&playerid={SystemPlayerData.Instance.uid}";
            var json = await wc.DownloadStringTaskAsync(remotePath);
            var data = JSONNode.Parse(json);

            var allItems = new List<ShopItem>();
            allItems.AddRange(_shopContent.SwordSkinItems);
            allItems.AddRange(_shopContent.BowSkinItems);

            for (int id = 0; id < allItems.Count; id++)
            {
                var item = allItems[id];
                var amount = data[id];
                Debug.Log(item + " " + amount);

                if (amount > 0 || id == 3)
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
    }
}