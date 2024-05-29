using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Inventory;
using Managers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryWindow : CommonWindow
{
    [SerializeField] private BlankSlot _blankInventorySlotPrefab;
    [SerializeField] private InventorySlot _itemInventorySlotPrefab;
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PlayerInventory _playerInventory;

    [Header("Right Bar")] [SerializeField] private TextMeshProUGUI _selectedItemName;
    [SerializeField] private TextMeshProUGUI _selectedItemDescription;

    [SerializeField] private Image _selectedItemImage;

    //[SerializeField] private Image _selectedItemBackgroundImage;
    [SerializeField] private TextMeshProUGUI _selectedItemRarity;
    [SerializeField] private TextMeshProUGUI _selectedItemAmount;

    private readonly int _maxInventoryItem = 20;
    private readonly Dictionary<InventoryItem, InventorySlot> _inventorySlots = new();

    private readonly Dictionary<InventoryItemCategory, List<(int index, InventoryItem item)>> _inventorySlotsPosition =
        new();

    private InventoryItem _selectedItem;
    private InventoryItemCategory _itemCategory = InventoryItemCategory.All;

    [Inject] private WindowManager _windowManager;

    private void Awake()
    {
        foreach (var itemCategory in typeof(InventoryItemCategory).GetEnumValues().Cast<InventoryItemCategory>())
        {
            _inventorySlotsPosition.Add(itemCategory, null);
        }
    }

    private void OnEnable() => Initialize();

    private void Initialize()
    {
        MakeBlankInventorySlots();
        MakeInventory();

        //ChangeSelectedItem(_inventorySlots.First().Key);
        _closeButton.onClick.AddListener(_windowManager.CloseInventoryWindow);
    }

    private void MakeBlankInventorySlots()
    {
        for (int i = 0; i < _maxInventoryItem; i++)
        {
            if (i >= _inventoryPanel.childCount)
            {
                var blankSlot = Instantiate(_blankInventorySlotPrefab, parent: _inventoryPanel);
                blankSlot.Initialize(i);
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
        var inventoryItems = OrderByRarity().ToList();
        if (_itemCategory != InventoryItemCategory.All)
        {
            inventoryItems = inventoryItems.Where(x => x.Category == _itemCategory)
                .ToList();
        }

        if (_inventorySlotsPosition[_itemCategory] == null ||
            inventoryItems.All(x => _inventorySlotsPosition[_itemCategory].Any(y => y.item == x)) == false)
        {
            var slotsPosition = new List<(int index, InventoryItem item)>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                var item = inventoryItems[i];

                if (item.Amount == 0)
                    continue;

                var parent = _inventoryPanel.GetChild(i);
                var slot = Instantiate(_itemInventorySlotPrefab, parent: parent);
                slot.Setup(item, this);
                _inventorySlots.Add(item, slot);
                slotsPosition.Add((i, item));
            }

            _inventorySlotsPosition[_itemCategory] = slotsPosition;
        }
        else
        {
            foreach (var slotData in _inventorySlotsPosition[_itemCategory])
            {
                if (slotData.item.Amount == 0)
                    continue;

                var parent = _inventoryPanel.GetChild(slotData.index);
                var slot = Instantiate(_itemInventorySlotPrefab, parent: parent);
                slot.Setup(slotData.item, this);

                _inventorySlots.Add(slotData.item, slot);
            }
        }
    }

    private IEnumerable<InventoryItem> OrderByRarity() => _playerInventory.InventoryItems.OrderBy(x => x.Rarity);

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
        //_selectedItemBackgroundImage.sprite = _inventorySlots[item].ItemBackgroundImage.sprite;

        _useButton.interactable = item.Usable;
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(OnUseButtonClick);
    }

    public void SelectCategory(InventoryItemCategory itemCategory)
    {
        _itemCategory = itemCategory;
        Initialize();
    }

    public void ChangeIndex(InventoryItem item, int newIndex)
    {
        var oldSlot = _inventorySlotsPosition[_itemCategory].First(x => x.item == item);
        _inventorySlotsPosition[_itemCategory].Remove(oldSlot);
        _inventorySlotsPosition[_itemCategory].Add((newIndex, item));
    }
}