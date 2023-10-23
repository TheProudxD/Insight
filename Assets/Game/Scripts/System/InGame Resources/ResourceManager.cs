using StorageService;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ResourceService
{
    public class ResourceManager : MonoBehaviour
    {
        [Inject] private StorageManager _storageManager;
        private ResourcesFeature _resourcesFeature;

        public static ResourceManager Instance;

        private void Awake()
        {
            if (Instance != null)
                return;
            Instance = this;
            DontDestroyOnLoad(this);

            var resSoft = new Resource(ResourceType.SoftCurrency, _storageManager.GetSoftCurrency());
            var resHard = new Resource(ResourceType.HardCurrency, _storageManager.GetHardCurrency());

            var resources = new[] { resHard, resSoft };
            _resourcesFeature = new ResourcesFeature(resources);
            _resourcesFeature.ResourceChanged += OnResourceChanged;

            /*
            foreach (var type in resources.Select(v=>v.Type))
            {
                print($"Created: Resource {type} = {_resourcesFeature.GetResourceValue(type)}");
            }
            */
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
            if (_resourcesFeature is null)
                return;
            _resourcesFeature.ResourceChanged -= OnResourceChanged;
        }

        public void Add(ResourceType resourceType, int rAmount)
        {
            _resourcesFeature.AddResource(resourceType, rAmount);
        }

        private void Spend(ResourceType resourceType, int rAmount)
        {
            _resourcesFeature.SpendResource(resourceType, rAmount);
        }

        private void OnResourceChanged(ResourceType resType, int oldV, int newV)
        {
            print($"Changed. Resource type: {resType}). Old value - {oldV}, new value - {newV}");
        }
    }
}