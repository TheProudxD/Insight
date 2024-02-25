using System.Linq;
using StorageService;
using UI.Shop.Data;
using Zenject;

namespace UI.Shop
{
    public class OpenedSkinsChecker : IShopItemVisitor
    {
        [Inject] private ShopData _shopData;

        public bool IsOpened { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(SwordSkinItem swordSkinItem) =>
            IsOpened = _shopData.OpenSwordSkins.Contains(swordSkinItem.SkinType);

        public void Visit(BowSkinItem bowSkinItem) =>
            IsOpened = _shopData.OpenedBowSkins.Contains(bowSkinItem.SkinType);
    }
}