using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.InGame_Resources
{
    public class ResourcesFeature
    {
        public readonly Dictionary<ResourceType, Resource> Resources;
        public event Action<ResourceType, int, int> ResourceChanged;
        
        public ResourcesFeature( Resource[] resources)
        {
            Resources = resources.ToDictionary(r=>r.Type);
            foreach (var resource in resources)
            {
                resource.Changed+= delegate(int oldValue, int newValue)
                {
                    ResourceChanged?.Invoke(resource.Type,oldValue, newValue);
                };
            }
        }

        public void AddResource(ResourceType type, int value)
        {
            var resource = Resources[type];
            resource.Amount += value;
        }
        
        public void SpendResource(ResourceType type, int value)
        {
            var resource = Resources[type];
            resource.Amount -= value;
        }
        
        public bool HasResource(ResourceType type, int value)
        {
            var resource = Resources[type];
            return resource.Amount >= value;
        }
        
        public string GetResourceValue(ResourceType type)
        {
            return Resources[type].Amount.ToString();
        }
    }
}