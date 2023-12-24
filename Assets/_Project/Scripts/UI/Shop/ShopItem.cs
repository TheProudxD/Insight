using UnityEngine;

namespace UI.Shop
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public Sprite Model { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        [field: SerializeField, Range(0, 10_000)] public int Price { get; private set; }
    }
}