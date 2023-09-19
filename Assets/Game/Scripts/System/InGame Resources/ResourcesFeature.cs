using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceService
{
    public class ResourcesFeature
    {
        public event Action<ResourceType, int, int> ResourceChanged;
        
        private readonly Dictionary<ResourceType, Resource> _resources;

        public ResourcesFeature(Resource[] resources)
        {
            _resources = resources.ToDictionary(r => r.Type);
            
            foreach (var resource in resources)
            {
                resource.Changed += delegate(int oldValue, int newValue)
                {
                    ResourceChanged?.Invoke(resource.Type, oldValue, newValue);
                };
            }
        }

        public void AddResource(ResourceType type, int value)
        {
            if (value < 0) throw new ArgumentException("Value cannot be negative");
            var resource = _resources[type];
            resource.Amount += value;
        }

        public void SpendResource(ResourceType type, int value)
        {
            if (value < 0) throw new ArgumentException("Value cannot be negative");
            var resource = _resources[type];
            resource.Amount -= value;
        }

        public bool HasResource(ResourceType type, int value)
        {
            var resource = _resources[type];
            return resource.Amount >= value;
        }

        public string GetResourceValue(ResourceType type)
        {
            return _resources[type].Amount.ToString();
        }
    }
}