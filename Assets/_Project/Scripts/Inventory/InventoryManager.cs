using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI _itemDescription;
    
    private int _maxInventoryItem = 20;
    private InventoryItem _selectedItemSlot;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        MakeBlankInventorySlots();
        MakeInventory();
    }

    private void MakeBlankInventorySlots()
    {
        for (int i = 0; i < _maxInventoryItem; i++)
        {
            Instantiate(_blankInventorySlotPrefab, parent: _inventoryPanel);
        }
    }

    private void MakeInventory()
    {
        for (int i = 0; i < _playerInventory.InventoryItems.Count; i++)
        {
            var parent = _inventoryPanel.GetChild(i);
            var slot = Instantiate(_itemInventorySlotPrefab, parent: parent);
            var item = _playerInventory.InventoryItems[i];
            slot.Setup(item, this);
        }
    }

    public void ChangeSelectedItem(InventoryItem slot)
    {
        _selectedItemSlot = slot;
        _itemDescription.SetText(slot.Description);
        _useButton.interactable = slot.Usable;
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    private void OnUseButtonClick()
    {
        _selectedItemSlot.Use();
    }
}