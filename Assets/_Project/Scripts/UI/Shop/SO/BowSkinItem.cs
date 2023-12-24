using UnityEngine;

namespace UI.Shop
{
    [CreateAssetMenu(fileName = "Bow Skin Item", menuName = "Shop/Weapon Type/Bow Skin Item")]
    public class BowSkinItem : ShopItem
    {
        [field: SerializeField] public BowSkins SkinType { get; private set; }
    }
}