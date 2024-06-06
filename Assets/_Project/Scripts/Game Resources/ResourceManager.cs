using System;
using System.Collections.Generic;
using System.Linq;
using Storage.Static;
using StorageService;
using UnityEngine;

namespace ResourceService
{
    public class ResourceManager
    {
        private const string CHANGE_HARD_CUR_KEY = "changehardcurrency";
        private const string CHANGE_SOFT_CUR_KEY = "changesoftcurrency";
        private const string CHANGE_ENERGY_KEY = "changeenergy";

        public event Action<ResourceType, int, int> ResourceChanged;

        private readonly IDynamicStorageService _dynamicStorageService;
        private Dictionary<ResourceType, Resource> _resources;
        private PlayerData _playerData;

        private ResourceManager(IDynamicStorageService dynamicStorageService, DataManager dataManager)
        {
            _dynamicStorageService = dynamicStorageService;
            dataManager.DataLoaded += Initialize;
        }

        public float MaxEnergyAmount => 50;

        private void Initialize(PlayerData playerData)
        {
            _playerData = playerData;

            Resource[] resources =
            {
                new(ResourceType.SoftCurrency, _playerData.AmountSoftResources),
                new(ResourceType.HardCurrency, _playerData.AmountHardResources),
                new(ResourceType.Energy, _playerData.AmountEnergy)
            };

            _resources = resources.ToDictionary(r => r.Type);

            ResourceChanged += OnResourceChanged;

            foreach (var resource in resources)
            {
                resource.Changed += (oldValue, newValue) =>
                {
                    ResourceChanged?.Invoke(resource.Type, oldValue, newValue);
                };
            }
        }

        private void OnResourceChanged(ResourceType resType, int oldV, int newV)
        {
            switch (resType)
            {
                case ResourceType.SoftCurrency:
                    SaveSoftCurrency(newV);
                    break;
                case ResourceType.HardCurrency:
                    SaveHardCurrency(newV);
                    break;
                case ResourceType.Energy:
                    SaveEnergy(newV);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resType), resType, null);
            }

            //Debug.Log($"Changed. Resource type: {resType}). Old value - {oldV}, new value - {newV}");
        }

        public void Add(ResourceType type, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be negative");

            var resource = _resources[type];
            var before = resource.Amount;
            resource.Amount += value;

            ResourceChanged?.Invoke(type, before, resource.Amount);
        }

        public void Spend(ResourceType type, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be negative");

            var resource = _resources[type];

            if (resource.Amount - value < 0)
                throw new ArgumentException("You can't spend more than is in current amount");

            var before = resource.Amount;
            resource.Amount -= value;

            ResourceChanged?.Invoke(type, before, resource.Amount);
        }

        private async void SaveSoftCurrency(int newValue)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playercurrency", newValue.ToString() },
                { "action", CHANGE_SOFT_CUR_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.AmountSoftResources = newValue;
                    Debug.Log($"Soft Currency saved Successfully to {newValue}");
                }
                else
                {
                    Debug.Log("Error while saving Soft Currency");
                }
            });
        }

        private async void SaveHardCurrency(int newValue)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playercurrency", newValue.ToString() },
                { "action", CHANGE_HARD_CUR_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.AmountHardResources = newValue;
                    Debug.Log($"Hard Currency saved Successfully to {newValue}");
                }
                else
                {
                    Debug.Log("Error while saving Hard Currency");
                }
            });
        }
        
        private async void SaveEnergy(int newValue)
        {
            var uploadParams = new Dictionary<string, string>
            {
                { "playercurrency", newValue.ToString() },
                { "action", CHANGE_ENERGY_KEY },
                { "playerid", SystemPlayerData.Instance.uid.ToString() },
            };

            await _dynamicStorageService.Upload(uploadParams, result =>
            {
                if (result)
                {
                    _playerData.AmountEnergy = newValue;
                    Debug.Log($"Energy saved Successfully to {newValue}");
                }
                else
                {
                    Debug.Log("Error while saving Energy");
                }
            });
        }

        public bool IsEnough(ResourceType type, int value) =>
            GetResourceValue(type) >= value;

        public int GetResourceValue(ResourceType type) =>
            _resources[type].Amount;

        ~ResourceManager() =>
            ResourceChanged -= OnResourceChanged;
    }
}