using System;
using _Project.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragAndDropSlot))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNumberText;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Button _itemButton;
    [SerializeField] private RarityBackgroundSprite _rarityBackgroundSprite;
    [field: SerializeField] public Image ItemBackgroundImage { get; private set; }

    private DragAndDropSlot _dragAndDrop;
    private InventoryItem _inventoryItem;
    private InventoryManager _inventoryManager;
    

    public void Setup(InventoryItem inventoryItem, InventoryManager inventoryManager)
    {
        _inventoryItem = inventoryItem;
        _inventoryManager = inventoryManager;
        _itemButton.onClick.AddListener(OnClick);
        _itemNumberText.SetText(_inventoryItem.Amount.ToString());
        _itemImage.sprite = _inventoryItem.Image;
        ItemBackgroundImage.sprite = inventoryItem.Rarity switch
        {
            InventoryItemRarity.Legendary => _rarityBackgroundSprite.GoldSprite,
            InventoryItemRarity.Epic => _rarityBackgroundSprite.RedSprite,
            InventoryItemRarity.Rare => _rarityBackgroundSprite.PurpleSprite,
            InventoryItemRarity.Uncommon => _rarityBackgroundSprite.GreenSprite,
            InventoryItemRarity.Common => _rarityBackgroundSprite.GraySprite,
            _ => throw new ArgumentOutOfRangeException()
        };

        _dragAndDrop = GetComponent<DragAndDropSlot>();
        _dragAndDrop.Initialize(this);
    }

    public void UpdateItemAmount() => _itemNumberText.SetText(_inventoryItem.Amount.ToString());

    private void OnClick() => _inventoryManager.ChangeSelectedItem(_inventoryItem);
}