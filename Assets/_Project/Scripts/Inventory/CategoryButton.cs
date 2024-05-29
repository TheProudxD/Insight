using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory
{
    [RequireComponent(typeof(Toggle))]
    public class CategoryButton : MonoBehaviour
    {
        [SerializeField] private InventoryItemCategory _itemCategory;
        [SerializeField] private InventoryWindow _inventoryWindow;

        private void Awake()
        {
            var selectCategoryButton = GetComponent<Toggle>();
            selectCategoryButton.onValueChanged.AddListener(SelectCategory);
        }

        private void SelectCategory(bool value)
        {
            if (value == false)
                return;

            _inventoryWindow.SelectCategory(_itemCategory);
        }
    }
}