using System;
using System.Collections.Generic;
using System.Linq;
using StorageService;
using UnityEngine;

namespace ResourceService
{
    public class ResourceManager
    {
        public event Action<ResourceType, int, int> ResourceChanged;

        private readonly Dictionary<ResourceType, Resource> _resources;

        public ResourceManager(int defaultSoft, int defaultHard)
        {
            Resource[] resources =
            {
                new(ResourceType.SoftCurrency, defaultSoft),
                new(ResourceType.HardCurrency, defaultHard)
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

        public void AddResource(ResourceType type, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be negative");
            var resource = _resources[type];
            resource.Amount += value;
        }

        public void SpendResource(ResourceType type, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be negative");
            var resource = _resources[type];
            resource.Amount -= value;
        }

        public bool HasResource(ResourceType type, int value) =>
            _resources[type].Amount >= value;

        public int GetResourceValue(ResourceType type) =>
            _resources[type].Amount;

        private void OnResourceChanged(ResourceType resType, int oldV, int newV) =>
            Debug.Log($"Changed. Resource type: {resType}). Old value - {oldV}, new value - {newV}");

        ~ResourceManager() =>
            ResourceChanged -= OnResourceChanged;
    }
}