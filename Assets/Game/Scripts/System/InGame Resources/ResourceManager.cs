using StorageService;
using System.Linq;
using UnityEngine;

namespace ResourceService
{
    public class ResourceManager : MonoBehaviour
    {
        private ResourcesFeature _resourcesFeature;

        public static ResourceManager Instance;
        private void Awake()
        {
            Instance = this;

            var resSoft = new Resource(ResourceType.SoftCurrency, StorageManager.GetSoftCurrency());
            var resHard = new Resource(ResourceType.HardCurrency, StorageManager.GetHardCurrency());

            var resources = new[] { resHard, resSoft };
            _resourcesFeature = new ResourcesFeature(resources);
            _resourcesFeature.ResourceChanged += OnResourceChanged;

            foreach (var type in resources.Select(v=>v.Type))
            {
                print($"Created: Resource {type} = {_resourcesFeature.GetResourceValue(type)}");
            }
        }

        private void Start()
        {
            /*
            StorageManager.SaveSoftCurrency(100);
            StorageManager.SaveHardCurrency(10);
            */
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