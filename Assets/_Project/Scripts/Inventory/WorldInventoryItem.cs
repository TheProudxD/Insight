using Tools;
using UnityEngine;

public class WorldInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private InventoryItem _pickupItem;
    [SerializeField] private int _amountToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utils.IsItPlayer(other) == false)
            return;
        
        AddItemToInventory();
        Destroy(gameObject);
    }

    private void AddItemToInventory()
    {
        if (_playerInventory.InventoryItems.Contains(_pickupItem) == false)
        {
            _playerInventory.InventoryItems.Add(_pickupItem);
            _pickupItem.Waste();
        }

        _pickupItem.Add(_amountToAdd);
    }
}