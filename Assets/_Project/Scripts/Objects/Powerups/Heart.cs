using Player;
using UnityEngine;

namespace Objects.Powerups
{
    public class Heart : Powerup
    {
        private readonly float _amountToIncrease = 2f;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player) && !other.isTrigger)
            {
                if (player.TryIncreaseHealth(_amountToIncrease))
                    Destroy(gameObject);
            }
        }
    }
}