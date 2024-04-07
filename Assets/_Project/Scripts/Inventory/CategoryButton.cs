using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory
{
    [RequireComponent(typeof(Button))]
    public class CategoryButton : MonoBehaviour
    {
        [SerializeField] private InventoryItemCategory _itemCategory;
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private Transform _slider;
        [SerializeField] private Button _selectCategoryButton;
        [field: SerializeField] public Image Image { get; private set; }

        private readonly Color _selectColor = new(0.2f, 0.64f, 0.98f, 1f);
        private readonly Color _defaultColor = Color.white;

        private List<CategoryButton> _allButtons;

        private void Awake()
        {
            _selectCategoryButton.onClick.AddListener(SelectCategory);
            _allButtons = FindObjectsOfType<CategoryButton>().ToList();
        }

        private void SelectCategory()
        {
            _slider.SetParent(transform);
            _allButtons.ForEach(x => x.Image.color = _defaultColor);
            Image.color = _selectColor;
            _slider.localPosition = new Vector3(0, -55, 0);
            _inventoryManager.SelectCategory(_itemCategory);
        }
    }
}