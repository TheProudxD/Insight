using Player;
using ResourceService;
using UnityEngine;
using Zenject;

namespace Objects.Powerups
{
    public class Coin : Powerup
    {
        private int _amountToIncrease;
        private ResourceManager _resourceManager;

        public void Initialize(CoinPowerupEntitySpecs coinPowerupEntitySpecs, ResourceManager resourceManager)
        {
            _amountToIncrease = coinPowerupEntitySpecs.Amount;
            _resourceManager = resourceManager;
            Destroy(gameObject, coinPowerupEntitySpecs.DestroyTimeAfterSpawn);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerAttacking player) && !other.isTrigger)
            {
                _resourceManager.AddResource(ResourceType.SoftCurrency, _amountToIncrease);
                Destroy(gameObject);
            }
        }
    }
}