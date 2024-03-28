using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNumberText;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _itemBackgroundImage;
    [SerializeField] private RarityBackgroundSprite _rarityBackgroundSprite;
    [SerializeField] private Button _itemButton;

    private InventoryItem InventoryItem { get; set; }
    private InventoryManager InventoryManager { get;  set; }

    public void Setup(InventoryItem inventoryItem, InventoryManager inventoryManager)
    {
        InventoryItem = inventoryItem;
        InventoryManager = inventoryManager;
        _itemButton.onClick.AddListener(OnClick);
        _itemNumberText.SetText(InventoryItem.Amount.ToString());
        _itemImage.sprite = InventoryItem.Image;
        _itemBackgroundImage.sprite = inventoryItem.Rarity switch
        {
            InventoryItemRarity.Gold => _rarityBackgroundSprite.GoldSprite,
            InventoryItemRarity.Red => _rarityBackgroundSprite.RedSprite,
            InventoryItemRarity.Purple => _rarityBackgroundSprite.PurpleSprite,
            InventoryItemRarity.Green => _rarityBackgroundSprite.GreenSprite,
            InventoryItemRarity.Gray => _rarityBackgroundSprite.GraySprite,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void UpdateItemAmount() => _itemNumberText.SetText(InventoryItem.Amount.ToString());

    private void OnClick() => InventoryManager.ChangeSelectedItem(InventoryItem);
}