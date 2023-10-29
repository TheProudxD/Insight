using System;
using StorageService;
using UnityEngine;
using Zenject;

namespace ResourceService
{
    public class ResourceGiver : MonoBehaviour
    {
        [Inject] private DataManager _dataManager;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private int _amount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (_resourceType)
            {
                case ResourceType.HardCurrency:
                    _dataManager.SetHardCurrency(_amount);
                    break;
                case ResourceType.SoftCurrency:
                    _dataManager.SetSoftCurrency(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Destroy(gameObject);
        }
    }
}