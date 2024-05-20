using DG.Tweening;
using Player;
using UnityEngine;

namespace Objects.Powerups
{
    public class Heart : Powerup
    {
        private float _amountToIncrease;
        private Tweener _tweener;

        public void Initialize(HeartPowerupEntitySpecs heartPowerupEntitySpecs)
        {
            _amountToIncrease = heartPowerupEntitySpecs.HealAmount;

            _tweener = transform.DOScale(Vector3.zero, heartPowerupEntitySpecs.DestroyTimeAfterSpawn)
                .OnComplete(() => Destroy(gameObject));
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player) && !other.isTrigger)
            {
                if (player.TryIncrease(_amountToIncrease))
                {
                    _tweener.Complete();
                    Destroy(gameObject);
                }
            }
        }
    }
}