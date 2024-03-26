using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Inventory/Create InventoryItem", fileName = "InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public bool Usable { get; private set; }
    [field: SerializeField] public bool Unique { get; private set; }
    [field: SerializeField] public ItemReaction ItemReaction { get; private set; }

    public void Use()
    {
        if (ItemReaction)
        {
            Debug.Log("Used: " + Name);
            ProjectContext.Instance.Container.InstantiatePrefab(ItemReaction).GetComponent<ItemReaction>().Use();
        }
    }
}