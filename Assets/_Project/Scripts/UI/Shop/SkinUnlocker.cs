using System.Linq;
using StorageService;
using UI.Shop.Data;
using Zenject;

namespace UI.Shop
{
    public class SkinUnlocker : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            _dataManager.ShopData.OpenSwordSkin(swordSkinItem.SkinType);

        public void Visit(BowSkinItem bowSkinItem) =>
            _dataManager.ShopData.OpenBowSkin(bowSkinItem.SkinType);
    }

    public class SkinSelector : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            _dataManager.ShopData.SelectedSwordSkins = swordSkinItem.SkinType;

        public void Visit(BowSkinItem bowSkinItem) =>
            _dataManager.ShopData.SelectedBowSkins = bowSkinItem.SkinType;
    }

    public class OpenedSkinsChecker : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;
        public bool IsOpened { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            IsOpened = _dataManager.ShopData.OpenSwordSkins.Contains(swordSkinItem.SkinType);

        public void Visit(BowSkinItem bowSkinItem) =>
            IsOpened = _dataManager.ShopData.OpenedBowSkins.Contains(bowSkinItem.SkinType);
    }

    public class SelectedSkinsChecker : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;
        public bool IsSelected { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            IsSelected = _dataManager.ShopData.SelectedSwordSkins == swordSkinItem.SkinType;

        public void Visit(BowSkinItem bowSkinItem) =>
            IsSelected = _dataManager.ShopData.SelectedBowSkins == bowSkinItem.SkinType;
    }
}