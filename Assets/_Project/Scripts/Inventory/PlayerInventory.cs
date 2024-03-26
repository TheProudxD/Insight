using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/New Inventory", fileName = "PlayerInventory", order = 0)]
public class PlayerInventory : ScriptableObject
{
    [field: SerializeField] public List<InventoryItem> InventoryItems { get; private set; } = new();
}