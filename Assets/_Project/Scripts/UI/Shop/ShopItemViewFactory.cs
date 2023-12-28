using System;
using UnityEngine;
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
}