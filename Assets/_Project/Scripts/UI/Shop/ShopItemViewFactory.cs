using System;
using StorageService;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UI.Shop
{
    [CreateAssetMenu(fileName = "Shop Item View Factory", menuName = "Shop/Shop Item View Factory", order = 1)]
    public class ShopItemViewFactory : ScriptableObject
    {
        [SerializeField] private ShopItemView _swordSkinItemPrefab;
        [SerializeField] private ShopItemView _bowSkinItemPrefab;

        public ShopItemView Get(ShopItem shopItem, Transform itemParent)
        {
            var visitor = new ShopItemVisitor(_swordSkinItemPrefab, _bowSkinItemPrefab);
            visitor.Visit(shopItem);
            var instance = Instantiate(visitor.Prefab, parent: itemParent);
            instance.Initialize(shopItem);
            return instance;
        }
    }

    public class ShopItemVisitor : IShopItemVisitor
    {
        [Inject] private DataManager _dataManager;
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