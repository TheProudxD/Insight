using UnityEngine;

namespace UI.Shop
{
    [CreateAssetMenu(fileName = "Sword Skin Item", menuName = "Shop/Weapon Type/Sword Skin Item")]
    public class SwordSkinItem : ShopItem
    {
        [field: SerializeField] public SwordSkins SkinType { get; private set; }
    }
}