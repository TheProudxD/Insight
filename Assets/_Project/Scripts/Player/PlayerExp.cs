using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerExp : PlayerFeature<int>
    {
        [SerializeField] private PlayerExpView _playerExpView;
        
        protected override void Awake()
        {
            base.Awake();
            _playerExpView.Initialize(MaxValue);
        }

        public override bool TryIncrease(int amount)
        {
            if (Math.Abs(Value - MaxValue) < 0.01f)
                return false;

            Signal.Raise(-amount);

            Value += amount;
            Value = Mathf.Clamp(Value, 0, MaxValue);

            return true;
        }

        public override void Decrease(int amount)
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