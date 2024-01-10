
namespace UI.Shop
{
    public class ShopItemVisitor : IShopItemVisitor
    {
        private readonly ShopItemView _sword;
        private readonly ShopItemView _bow;

        public ShopItemVisitor(ShopItemView sword, ShopItemView bow)
        {
            _sword = sword;
            _bow = bow;
        }

        public ShopItemView Prefab { get; set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            Prefab = _sword;

        public void Visit(BowSkinItem bowSkinItem) =>
            Prefab = _bow;
    }
}