using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Inventory/Create InventoryItem", fileName = "InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public bool Usable { get; private set; }
    [field: SerializeField] public bool Unique { get; private set; }
    [field: SerializeField] public ItemReaction ItemReaction { get; private set; }
    [field: SerializeField] public InventoryItemRarity Rarity { get; private set; }
    [field: SerializeField] public InventoryItemCategory Category { get; private set; }

    public bool TryUse(int amount)
    {
        if (Amount - amount < 0)
            return false;

        if (ItemReaction)
        {
            Spend(amount);
            ProjectContext.Instance.Container.InstantiatePrefab(ItemReaction).GetComponent<ItemReaction>().Use();
        }

        return true;
    }

    public void Add(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException(nameof(amount));

        Amount += amount;
    }

    public void Spend(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException(nameof(amount));

        Amount -= amount;
    }

    public void StartAmount(int amount) => Amount = amount;
}