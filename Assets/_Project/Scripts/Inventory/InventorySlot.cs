using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNumberText;
    [SerializeField] private Image _itemImage;
    [field: SerializeField] public Button ItemButton { get; private set; }

    private InventoryItem InventoryItem { get; set; }
    private InventoryManager InventoryManager { get;  set; }

    public void Setup(InventoryItem inventoryItem, InventoryManager inventoryManager)
    {
        InventoryItem = inventoryItem;
        InventoryManager = inventoryManager;
        ItemButton.onClick.AddListener(OnClick);
        _itemNumberText.SetText(inventoryItem.ID.ToString());
        _itemImage.sprite = inventoryItem.Image;
    }

    private void OnClick()
    {
        InventoryManager.ChangeSelectedItem(InventoryItem);
    }
}