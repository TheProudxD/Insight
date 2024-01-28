using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : PlayerFeature
    {
        public bool TryIncrease(float amount)
        {
            if (Math.Abs(Value - MaxValue) < 0.01f)
                return false;
            
            Signal.Raise(-amount);

            Value += amount;
            Value = Mathf.Clamp(Value, 0, MaxValue);
            return true;
        }

        public void Decrease(float amount)
        {
            if (Value <= 0)
                return;

            Signal.Raise();
            Signal.Raise(amount);
            Value -= amount;

            if (Value <= 0)
            {
                StartCoroutine(GameStateManager.Instance.GameOver());
            }
        }
    }
}