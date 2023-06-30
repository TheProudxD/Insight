using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.InGame_Resources
{
    public class ExampleRes: MonoBehaviour
    {
        private ResourcesFeature _resourcesFeature;
        private void Start()
        {
            var resSoft = new Resource(ResourceType.SoftCurrency, 10);
            var resHard = new Resource(ResourceType.HardCurrency, 5);

            var resources = new[] { resHard, resSoft };
            _resourcesFeature = new ResourcesFeature(resources);
            _resourcesFeature.ResourceChanged += OnResourceChanged;
            
            print($"Created: Resource {ResourceType.SoftCurrency} = {_resourcesFeature.GetResourceValue(ResourceType.SoftCurrency)}");
            print($"Created: Resource {ResourceType.HardCurrency} = {_resourcesFeature.GetResourceValue(ResourceType.HardCurrency)}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddRandom();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SpendRandom();
            }
        }

        private void AddRandom()
        {
            var resources = new[] { ResourceType.SoftCurrency, ResourceType.HardCurrency };
            var rIndex = Random.Range(0, resources.Length);
            var rResourceType = resources[rIndex];
            var rAmount = Random.Range(0, 100);
            _resourcesFeature.AddResource(rResourceType, rAmount);
        }

        private void SpendRandom()
        {
            var resources = new[] { ResourceType.SoftCurrency, ResourceType.HardCurrency };
            var rIndex = Random.Range(0, resources.Length);
            var rResourceType = resources[rIndex];
            var rAmount = Random.Range(0, 100);
            _resourcesFeature.SpendResource(rResourceType, rAmount);
        }

        private void OnResourceChanged(ResourceType resType, int oldV, int newV)
        {
            print($"Changed. Resource type: {resType}). Old value - {oldV}, new value - {newV}");
        }

        private void OnDestroy()
        {
            _resourcesFeature.ResourceChanged -= OnResourceChanged;
        }
    }
}