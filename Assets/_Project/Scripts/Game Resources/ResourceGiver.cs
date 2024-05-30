using UnityEngine;
using Zenject;

namespace ResourceService
{
    public class ResourceGiver : MonoBehaviour
    {
        [Inject] private ResourceManager _resourceManager;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private int _amount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _resourceManager.Add(_resourceType, _amount);

            Destroy(gameObject);
        }
    }
}