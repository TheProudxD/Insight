using Objects.Powerups;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class Coin : Powerup
    {
        private float _amountToIncrease = 2f;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController player) && !other.isTrigger)
            {
                Inventory.NumberOfCoins++;
                print(Inventory.NumberOfCoins);
                Destroy(gameObject);
            }
        }
    }
}