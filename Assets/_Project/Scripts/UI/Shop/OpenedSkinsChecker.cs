using System.Linq;
using StorageService;
using UnityEditor.Build.Pipeline.Injector;
using UnityEngine;
using Zenject;

namespace UI.Shop
{
    public class OpenedSkinsChecker : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;

        public bool IsOpened { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem)
        {
            if (_dataManager != null)
                IsOpened = _dataManager.ShopData.OpenSwordSkins.Contains(swordSkinItem.SkinType);
            else
                Debug.Log(123);
        }

        public void Visit(BowSkinItem bowSkinItem) =>
            IsOpened = _dataManager.ShopData.OpenedBowSkins.Contains(bowSkinItem.SkinType);
    }
}