using Player;
using UnityEngine;

namespace Objects.Powerups
{
    public class Heart : Powerup
    {
        private float _amountToIncrease;

        public void Initialize(HeartPowerupEntitySpecs heartPowerupEntitySpecs)
        {
            _amountToIncrease = heartPowerupEntitySpecs.HealAmount;
            Destroy(gameObject, heartPowerupEntitySpecs.DestroyTimeAfterSpawn);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player) && !other.isTrigger)
            {
                if (player.TryIncrease(_amountToIncrease))
                    Destroy(gameObject);
            }
        }
    }
}