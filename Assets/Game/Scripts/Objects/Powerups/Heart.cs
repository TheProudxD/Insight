using Player;
using UnityEngine;

namespace Objects.Powerups
{
    namespace Objects.Powerups
    {
        public class Heart : Powerup
        {
            private float _amountToIncrease = 2f;

            protected void OnTriggerEnter2D(Collider2D other)
            {
                if (other.TryGetComponent(out PlayerController player) && !other.isTrigger)
                {
                    if (player.TryIncreaseHealth(_amountToIncrease))
                        Destroy(gameObject);
                }
            }
        }
    }
}