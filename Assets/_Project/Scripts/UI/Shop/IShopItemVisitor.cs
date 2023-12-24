namespace UI.Shop
{
    public interface IShopItemVisitor
    {
        void Visit(ShopItem shopItem);
        void Visit(SwordSkinItem swordSkinItem);
        void Visit(BowSkinItem bowSkinItem);
    }
}