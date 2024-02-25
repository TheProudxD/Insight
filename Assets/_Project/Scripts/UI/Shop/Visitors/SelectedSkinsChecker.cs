using UI.Shop.Data;
using Zenject;

namespace UI.Shop
{
    public class SelectedSkinsChecker : IShopItemVisitor
    {
        [Inject] private ShopData _shopData;
        public bool IsSelected { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            IsSelected = _shopData.SelectedSwordSkins == swordSkinItem.SkinType;

        public void Visit(BowSkinItem bowSkinItem) =>
            IsSelected = _shopData.SelectedBowSkins == bowSkinItem.SkinType;
    }
}