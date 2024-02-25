using ResourceService;
using UnityEngine;

namespace UI.Shop
{
    public abstract class ShopItem : ScriptableObject
    {
        public abstract int ID { get; }
        [field: SerializeField] public Sprite Model { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        
        [field: SerializeField] public ResourceType ResourceType { get; set; }
        [field: SerializeField, Range(0, 10_000)] public int Price { get; private set; }
    }
}