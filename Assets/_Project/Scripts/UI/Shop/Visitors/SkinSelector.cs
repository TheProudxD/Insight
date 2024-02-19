using StorageService;
using Zenject;

namespace UI.Shop
{
    public class SkinSelector : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            _dataManager.ShopData.SelectedSwordSkins = swordSkinItem;

        public void Visit(BowSkinItem bowSkinItem) =>
            _dataManager.ShopData.SelectedBowSkins = bowSkinItem;
    }
}