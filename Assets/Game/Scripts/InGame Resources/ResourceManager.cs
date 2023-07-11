using System.Linq;
using UnityEngine;

namespace Game.Scripts.InGame_Resources
{
    public class ResourceManager : MonoBehaviour
    {
        private ResourcesFeature _resourcesFeature;

        public static ResourceManager Instance;
        private void Awake()
        {
            Instance = this;
            
            var resSoft = new Resource(ResourceType.SoftCurrency, 100);
            var resHard = new Resource(ResourceType.HardCurrency, 5);

            var resources = new[] { resHard, resSoft };
            _resourcesFeature = new ResourcesFeature(resources);
            _resourcesFeature.ResourceChanged += OnResourceChanged;

            foreach (var type in resources.Select(v=>v.Type))
            {
                print($"Created: Resource {type} = {_resourcesFeature.GetResourceValue(type)}");
            }
        }

        private void OnDestroy()
        {
            _resourcesFeature.ResourceChanged -= OnResourceChanged;
        }

        public void AddRandom(ResourceType resourceType)
        {
            var rAmount = Random.Range(0, 100);
            _resourcesFeature.AddResource(resourceType, rAmount);
        }

        private void SpendRandom(ResourceType resourceType)
        {
            var rAmount = Random.Range(0, 100);
            _resourcesFeature.SpendResource(resourceType, rAmount);
        }

        private void OnResourceChanged(ResourceType resType, int oldV, int newV)
        {
            print($"Changed. Resource type: {resType}). Old value - {oldV}, new value - {newV}");
        }
    }
}