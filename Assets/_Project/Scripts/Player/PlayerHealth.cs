using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : PlayerFeature<float>
    {
        public override float Amount
        {
            get => PlayerEntitySpecs.HpAmount;
            protected set => PlayerEntitySpecs.HpAmount = value;
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            MaxAmount = PlayerEntitySpecs.HpAmount;
        }

        public override bool TryIncrease(float amount)
        {
            if (Math.Abs(Amount - MaxAmount) < 0.01f)
                return false;
            
            Signal.Raise(-amount);

            Amount += amount;
            Amount = Mathf.Clamp(Amount, 0, MaxAmount);
            return true;
        }

        public override void Decrease(float amount)
        {
            if (Amount <= 0)
                return;

            Signal.Raise();
            Signal.Raise(amount);
            Amount -= amount;

            if (Amount <= 0)
            {
                StartCoroutine(GameStateManager.Instance.GameOver());
            }
        }
    }
}