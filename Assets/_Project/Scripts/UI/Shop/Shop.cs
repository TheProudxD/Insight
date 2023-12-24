using UnityEngine;

namespace UI.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopContent _shopContent;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private ShopCategoryButton _swordCategoryButton;
        [SerializeField] private ShopCategoryButton _bowCategoryButton;

        private void OnEnable()
        {
            _swordCategoryButton.Click += OnSwordSkinButtonClick;
            _bowCategoryButton.Click += OnBowSkinButtonClick;
        }

        private void OnSwordSkinButtonClick()
        {
            _swordCategoryButton.Select();
            _bowCategoryButton.Unselect();
            _shopPanel.Show(_shopContent.SwordSkinItems);
        }
        
        private void OnBowSkinButtonClick()
        {
            _bowCategoryButton.Select();
            _swordCategoryButton.Unselect();
            _shopPanel.Show(_shopContent.BowSkinItems);
        }
    }
}