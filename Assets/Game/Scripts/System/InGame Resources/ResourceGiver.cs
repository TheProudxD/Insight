using UnityEngine;

namespace ResourceService
{
    public class ResourceGiver : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            //ResourceManager.Instance.AddRandom(_resourceType);
            Destroy(gameObject);
        }
    }
}