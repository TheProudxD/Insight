using System;
using System.Collections.Generic;
using System.Linq;
using ResourceService;
using StorageService;
using Zenject;

namespace UI.Shop.Data
{
    public class ShopData
    {
        private BowSkins _selectedBowSkins;
        private SwordSkins _selectedSwordSkins;

        private List<BowSkins> _openedBowSkins;
        private List<SwordSkins> _openedSwordSkins;

        [Inject] private DataManager _dataManager;
        //private int _money;
        
        public ShopData()
        {
            //_money = ;
            _selectedBowSkins = BowSkins.Common;
            _selectedSwordSkins = SwordSkins.Common;
            _openedBowSkins = new List<BowSkins> { _selectedBowSkins };
            _openedSwordSkins = new List<SwordSkins> { _selectedSwordSkins };
        }

        public int Money
        {
            get => _dataManager.ResourceManager.GetResourceValue(ResourceType.SoftCurrency);
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                
                // _money = value;
            }
        }

        public BowSkins SelectedBowSkins
        {
            get => _selectedBowSkins;
            set
            {               
                if (_openedBowSkins.Contains(value) == false)
                    throw new ArgumentException();
                _selectedBowSkins = value;
            }
        }

        public SwordSkins SelectedSwordSkins
        {
            get => _selectedSwordSkins;
            set
            {if (_openedSwordSkins.Contains(value) == false)
                    throw new ArgumentException();
                _selectedSwordSkins = value;
            }
        }

        public IEnumerable<BowSkins> OpenedBowSkins => _openedBowSkins;

        public IEnumerable<SwordSkins> OpenSwordSkins => _openedSwordSkins;

        public void OpenSwordSkin(SwordSkins skin)
        {
            if (_openedSwordSkins.Contains(skin))
                throw new ArgumentException(nameof(skin));
            
            _openedSwordSkins.Add(skin);
        }
        
        public void OpenBowSkin(BowSkins skin)
        {
            if (_openedBowSkins.Contains(skin))
                throw new ArgumentException(nameof(skin));
            
            _openedBowSkins.Add(skin);
        }
    }
}