using System.Collections.Generic;
using System.Threading.Tasks;
using Assets._Project.Scripts.Storage.Static;
using Game.Scripts.Storage;
using ResourceService;
using UnityEngine;
using static Tools.Utils;

namespace StorageService
{
    public class DataManager
    {
        public const string REGISTRY_DATA_KEY = "registry";
        public const string MAX_LEVEL_DATA_KEY = "maxleveldata";
        public const string DYNAMIC_USER_DATA_KEY = "userdata";

        private readonly IStaticStorageService _staticStorageService;
        private readonly IDynamicStorageService _dynamicStorageService;
        private readonly ResourceManager _resourceManager;
        private readonly LevelManager _levelManager;

        private StaticPlayerData _staticPlayerData;
        private DynamicPlayerData _dynamicPlayerData;

        public DataManager(IStaticStorageService staticStorageService, IDynamicStorageService dynamicStorageService,
            ResourceManager resourceManager, LevelManager levelManager)
        {
            _staticStorageService = staticStorageService;
            _dynamicStorageService = dynamicStorageService;
            _resourceManager = resourceManager;
            _levelManager = levelManager;
        }
        
        public async Task SetName(string newName)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playername", newName },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
                { "action", "changename" },
            };
        
            await _dynamicStorageService.Upload(uploadParams, result=>
            {
                if (result)
                {
                    _dynamicPlayerData.Name = newName;
                    Debug.Log("Renaming Successfully");
                }
                else
                {
                    Debug.Log("Error while renaming");                
                }
            
            });
        }
        
        public void SetLevel(int level)
        {
            _levelManager.SetCurrentLevel(level);
            //_dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData, b => Print("Level saved successfully!"));
        }

        public void SetSoftCurrency(int amount)
        {
            _resourceManager.AddResource(ResourceType.SoftCurrency, amount);
            //SaveSoftCurrency();
        }

        public void SetHardCurrency(int amount)
        {
            _resourceManager.AddResource(ResourceType.HardCurrency, amount);
            //SaveHardCurrency();
        }
        
        /*
        private void SaveSoftCurrency()
        {
            _dynamicData.AmountSoftResources = GetSoftCurrencyAmount();
            _dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData,
                b => Print("Soft Currency saved successfully!"));
        }

        private void SetMaxLevel(int maxLevel)
        {
            _staticPlayerData.MaxLevel = maxLevel;
            _staticStorageService.Upload(MAX_LEVEL_DATA_KEY, _staticPlayerData, b => Print("Max Level saved successfully!"));
        }

        private void SaveHardCurrency()
        {
            _dynamicData.AmountHardResources = GetHardCurrencyAmount();
            _dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData,
                b => Print("Hard Currency saved successfully!"));
        }

        private void SaveCurrentLevel()
        {
            _dynamicData.CurrentLevel = _levelManager.CurrentLevel;
            _dynamicStorageService.Upload(DYNAMIC_USER_DATA_KEY, _dynamicData,
                b => Print("Current Level saved successfully!"));
                
        }
        */
        public int GetCurrentLevel() => _levelManager.GetCurrentLevel();

        public int GetMaxLevel() => _staticPlayerData.MaxLevel;

        public int GetSoftCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.SoftCurrency);

        public int GetHardCurrencyAmount() => _resourceManager.GetResourceValue(ResourceType.HardCurrency);

        public string GetName() => _dynamicPlayerData.Name;

        public void SetData(StaticPlayerData playerData) => _staticPlayerData = playerData;

        public void SetData(DynamicPlayerData playerData) => _dynamicPlayerData = playerData;
    }
}