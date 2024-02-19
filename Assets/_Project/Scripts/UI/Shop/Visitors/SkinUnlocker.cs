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
            _dataManager.ShopData.OpenSwordSkin(swordSkinItem);

        public void Visit(BowSkinItem bowSkinItem) =>
            _dataManager.ShopData.OpenBowSkin(bowSkinItem);
    }
}