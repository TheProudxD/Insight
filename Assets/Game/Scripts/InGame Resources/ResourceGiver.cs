using System;
using UnityEngine;

namespace Game.Scripts.InGame_Resources
{
    public class ResourceGiver : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            ResourceManager.Instance.AddRandom(_resourceType);
            Destroy(gameObject);
        }
    }
}