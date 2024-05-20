using DG.Tweening;
using Player;
using ResourceService;
using UnityEngine;

namespace Objects.Powerups
{
    public class Coin : Powerup
    {
        private int _amountToIncrease;
        private ResourceManager _resourceManager;
        private Tweener _tweener;

        public void Initialize(CoinPowerupEntitySpecs coinPowerupEntitySpecs, ResourceManager resourceManager)
        {
            _amountToIncrease = coinPowerupEntitySpecs.Amount;
            _resourceManager = resourceManager;
            
            _tweener = transform.DOScale(Vector3.zero, coinPowerupEntitySpecs.DestroyTimeAfterSpawn).OnComplete(()=>Destroy(gameObject));
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerAttacking player) && !other.isTrigger)
            {
                _resourceManager.AddResource(ResourceType.SoftCurrency, _amountToIncrease);
                _tweener.Complete();
                Destroy(gameObject);
            }
        }
    }
}