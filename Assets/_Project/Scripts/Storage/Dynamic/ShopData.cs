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
        [Inject] private ShopContent _shopContent;

        [Inject]
		public ShopData(ShopContent shopContent)
		{
			_shopContent = shopContent;
		}

		private BowSkinItem _selectedBowSkins;
        private SwordSkinItem _selectedSwordSkins;

        private List<BowSkinItem> _openedBowSkins => _openedItems.Keys.Where(x=>(BowSkinItem)x!=null).Select(x=> (BowSkinItem)x).ToList();
        private List<SwordSkinItem> _openedSwordSkins=> _openedItems.Keys.Where(x=>(SwordSkinItem)x!=null).Select(x=> (SwordSkinItem)x).ToList();
        private readonly Dictionary<ShopItem, int> _openedItems;
        private IDynamicStorageService _dynamicStorageService { get; set; }

        public BowSkinItem SelectedBowSkins
        {
            get => _selectedBowSkins;
            set
            {               
                if (_openedBowSkins.Contains(value) == false)
                    throw new ArgumentException();

                _selectedBowSkins = value;
            }
        }

        public SwordSkinItem SelectedSwordSkins
        {
            get => _selectedSwordSkins;
            set
            {
                if (_openedSwordSkins.Contains(value) == false)
                    throw new ArgumentException();
                _selectedSwordSkins = value;
            }
        }

        public IEnumerable<BowSkinItem> OpenedBowSkins => _openedBowSkins;

        public IEnumerable<SwordSkinItem> OpenSwordSkins => _openedSwordSkins;

        public ShopData(IDynamicStorageService dynamicStorageService)
        {
            //_selectedBowSkins = BowSkins.Common;
            //_selectedSwordSkins = SwordSkins.Common;

            _dynamicStorageService = dynamicStorageService;

            _openedItems = new(_shopContent.AllSkinItemsAmount);
			foreach (var item in _shopContent.AllSkinItems)
			{
                _openedItems.Add(item, 0);
			}

            GetItems();
        }

        private async void GetItems() => await LoadItems();

        public void OpenSwordSkin(SwordSkinItem item)
        {
            if (_openedSwordSkins.Contains(item))
                throw new ArgumentException(nameof(item));

            _selectedSwordSkins = item;
            _openedSwordSkins.Add(item);
            SaveNewItem(item);
        }
        
        public void OpenBowSkin(BowSkinItem item)
        {
            if (_openedBowSkins.Contains(item))
                throw new ArgumentException(nameof(item));

            _selectedBowSkins = item;
            _openedBowSkins.Add(item);
            SaveNewItem(item);
        }

        private async void SaveNewItem(ShopItem skin)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { $"itemid", skin.ID.ToString() },
                { $"itemamount", "1" },
                { "action", "changeitems" },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
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

        public async Task LoadItems()
        {
            using var wc = new WebClient();
            var remotePath = "http://game.ispu.ru/insight" + $"/api.php?action=getitems&playerid={SystemPlayerData.Instance.uid}";
            var json = await wc.DownloadStringTaskAsync(remotePath);
            var data = JSONNode.Parse(json);

            var boughtIdAmount = new List<int>
            {
                int.Parse(data[0]),
                int.Parse(data[1]),
                int.Parse(data[2]),
                int.Parse(data[3]),
                int.Parse(data[4]),
                int.Parse(data[5])
            };

            for (int i = 0; i < boughtIdAmount.Count; i++)
			{
				int amount = boughtIdAmount[i];

                if (amount > 0)
                {
                   _openedItems[_shopContent.AllSkinItems.First(x=>x.ID== boughtIdAmount[i])] = amount;
                    Debug.Log(i);
                }
            }
        }
    }
}