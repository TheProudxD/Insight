using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Image _blankInventorySlotPrefab;
    [SerializeField] private InventorySlot _itemInventorySlotPrefab;
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private Button _useButton;
    [SerializeField] private PlayerInventory _playerInventory;

    [Header("Right Bar")] [SerializeField] private TextMeshProUGUI _selectedItemName;
    [SerializeField] private TextMeshProUGUI _selectedItemDescription;
    [SerializeField] private Image _selectedItemImage;
    [SerializeField] private Image _selectedItemBackgroundImage;
    [SerializeField] private TextMeshProUGUI _selectedItemRarity;
    [SerializeField] private TextMeshProUGUI _selectedItemAmount;

    private readonly int _maxInventoryItem = 20;
    private readonly Dictionary<InventoryItem, InventorySlot> _inventorySlots = new();
    private InventoryItem _selectedItem;

    private void OnEnable() => Initialize();

    private void Initialize()
    {
        MakeBlankInventorySlots();
        MakeInventory();
    }

    private void MakeBlankInventorySlots()
    {
        for (int i = 0; i < _maxInventoryItem; i++)
        {
            if (i >= _inventoryPanel.childCount)
            {
                Instantiate(_blankInventorySlotPrefab, parent: _inventoryPanel);
            }
            else
            {
                var slot = _inventoryPanel.GetChild(i);
                for (int j = 0; j < slot.childCount; j++)
                    Destroy(slot.GetChild(j).gameObject);
            }
        }

        _inventorySlots.Clear();
    }

    private void MakeInventory()
    {
        var inventoryItems = OrderByRarity();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var item = inventoryItems[i];

            if (item.Amount == 0)
                continue;

            var parent = _inventoryPanel.GetChild(i);
            var slot = Instantiate(_itemInventorySlotPrefab, parent: parent);
            slot.Setup(item, this);
            _inventorySlots.Add(item, slot);
        }
    }

    private List<InventoryItem> OrderByRarity() => _playerInventory.InventoryItems.OrderBy(x => x.Rarity).ToList();

    private void OnUseButtonClick()
    {
        if (!_selectedItem.TryUse(1))
            return;

        _inventorySlots.TryGetValue(_selectedItem, out var inventorySlot);

        if (inventorySlot == null)
        {
            Debug.LogError("Inventory item is null");
            return;
        }

        inventorySlot.UpdateItemAmount();

        if (_selectedItem.Amount <= 0)
        {
            _inventorySlots.Remove(_selectedItem);
            Destroy(inventorySlot.gameObject);
        }
    }

    public void ChangeSelectedItem(InventoryItem item)
    {
        _selectedItem = item;

        _selectedItemName.SetText(item.Name);
        _selectedItemDescription.SetText(item.Description);
        _selectedItemRarity.SetText(item.Rarity.ToString());
        _selectedItemAmount.SetText(item.Amount.ToString());
        _selectedItemImage.sprite = item.Image;
        _selectedItemBackgroundImage.sprite = _inventorySlots[item].ItemBackgroundImage.sprite;

        _useButton.interactable = item.Usable;
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(OnUseButtonClick);
    }
}