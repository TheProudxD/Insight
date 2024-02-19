using StorageService;
using Zenject;

namespace UI.Shop
{
    public class SelectedSkinsChecker : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;
        public bool IsSelected { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            IsSelected = _dataManager.ShopData.SelectedSwordSkins == swordSkinItem;

        public void Visit(BowSkinItem bowSkinItem) =>
            IsSelected = _dataManager.ShopData.SelectedBowSkins == bowSkinItem;
    }
}