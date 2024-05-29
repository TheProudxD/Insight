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
    public InventoryItem InventoryItem { get; private set; }
    private InventoryWindow _inventoryWindow;

    public void Setup(InventoryItem inventoryItem, InventoryWindow inventoryWindow)
    {
        InventoryItem = inventoryItem;
        _inventoryWindow = inventoryWindow;
        _itemButton.onClick.AddListener(OnClick);
        _itemNumberText.SetText(InventoryItem.Amount.ToString());
        _itemImage.sprite = InventoryItem.Image;
        /*
        ItemBackgroundImage.sprite = inventoryItem.Rarity switch
        {
            InventoryItemRarity.Legendary => _rarityBackgroundSprite.GoldSprite,
            InventoryItemRarity.Epic => _rarityBackgroundSprite.RedSprite,
            InventoryItemRarity.Rare => _rarityBackgroundSprite.PurpleSprite,
            InventoryItemRarity.Uncommon => _rarityBackgroundSprite.GreenSprite,
            InventoryItemRarity.Common => _rarityBackgroundSprite.GraySprite,
            _ => throw new ArgumentOutOfRangeException()
        };
        */
        _dragAndDrop = GetComponent<DragAndDropSlot>();
        _dragAndDrop.Initialize(this);
    }

    public void UpdateItemAmount() => _itemNumberText.SetText(InventoryItem.Amount.ToString());

    private void OnClick() => _inventoryWindow.ChangeSelectedItem(InventoryItem);
}