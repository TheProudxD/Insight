using System;
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

            _tweener = transform.DOScale(Vector3.zero, coinPowerupEntitySpecs.DestroyTimeAfterSpawn)
                .OnComplete(Destroy);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerAttacking player) && !other.isTrigger)
            {
                _resourceManager.Add(ResourceType.SoftCurrency, _amountToIncrease);
                Destroy();
            }
        }

        private void Destroy()
        {
            _tweener.Kill();
            Destroy(gameObject);
        }
    }
}