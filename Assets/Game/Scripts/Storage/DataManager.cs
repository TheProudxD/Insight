using Game.Scripts.Storage;
using ResourceService;
using static Tools.Utils;

namespace StorageService
{
    public class DataManager
    {
        public const string STATIC_DATA_KEY = "AllData";
        public const string DYNAMIC_DATA_KEY = "alldata";

        private readonly IStaticStorageService _staticStorageService;
        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly ResourceManager _resourceManager;
        private readonly LevelManager _levelManager;

        private StaticData _staticData;
        private DynamicData _dynamicData;

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService,
            ResourceManager resourceManager, LevelManager levelManager)
        {
            _staticStorageService = staticStorageService;
            _dynamicStorageService = dynamicStorageService;
            _resourceManager = resourceManager;
            _levelManager = levelManager;
        }

        public void SetLevel(int level)
        {
            _levelManager.SetCurrentLevel(level);
            _dynamicStorageService.Save(DYNAMIC_DATA_KEY, _dynamicData, b => Print("Level saved successfully!"));
        }

        public void SetSoftCurrency(int amount)
        {
            _resourceManager.AddResource(ResourceType.SoftCurrency, amount);
            SaveSoftCurrency();
        }

        public void SetHardCurrency(int amount)
        {
            _resourceManager.AddResource(ResourceType.HardCurrency, amount);
            SaveHardCurrency();
        }

        private void SaveSoftCurrency()
        {
            _dynamicData.AmountSoftResources = GetSoftCurrencyAmount();
            _dynamicStorageService.Save(DYNAMIC_DATA_KEY, _dynamicData,
                b => Print("Soft Currency saved successfully!"));
        }

        private void SetMaxLevel(int maxLevel)
        {
            _staticData.MaxLevel = maxLevel;
            _staticStorageService.Save(STATIC_DATA_KEY, _staticData, b => Print("Current Level saved successfully!"));
        }

        private void SaveHardCurrency()
        {
            _dynamicData.AmountHardResources = GetHardCurrencyAmount();
            _dynamicStorageService.Save(DYNAMIC_DATA_KEY, _dynamicData,
                b => Print("Hard Currency saved successfully!"));
        }

        private void SaveCurrentLevel()
        {
            _dynamicData.CurrentLevel = _levelManager.CurrentLevel;
            _dynamicStorageService.Save(DYNAMIC_DATA_KEY, _dynamicData,
                b => Print("Current Level saved successfully!"));
        }

        public int GetCurrentLevel() => _levelManager.GetCurrentLevel();

        public int GetMaxLevel() => _staticData.MaxLevel;

        public int GetSoftCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.SoftCurrency);

        public int GetHardCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.HardCurrency);

        public void SetData(StaticData data) => _staticData = data;

        public void SetData(DynamicData data) => _dynamicData = data;
    }
}