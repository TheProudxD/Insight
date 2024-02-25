using StorageService;
using UI.Shop.Data;
using Zenject;

namespace UI.Shop
{
    public class SkinUnlocker : IShopItemVisitor
    {
        [Inject] private ShopData _shopData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            _shopData.OpenSwordSkin(swordSkinItem.SkinType);

        public void Visit(BowSkinItem bowSkinItem) =>
            _shopData.OpenBowSkin(bowSkinItem.SkinType);
    }
}