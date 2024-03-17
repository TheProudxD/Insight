using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerExp : PlayerFeature<int>
    {
        [SerializeField] private PlayerExpView _playerExpView;

        public override float Amount
        {
            get => PlayerEntitySpecs.ExperienceAmount;
            protected set => PlayerEntitySpecs.ExperienceAmount = value;
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            MaxAmount = PlayerEntitySpecs.ExperienceAmount;
            _playerExpView.Initialize(MaxAmount);
        }

        public override bool TryIncrease(int amount)
        {
            if (Math.Abs(Amount - MaxAmount) < 0.01f)
                return false;

            Signal.Raise(-amount);

            Amount += amount;
            Amount = Mathf.Clamp(Amount, 0, MaxAmount);

            return true;
        }

        public override void Decrease(int amount) => throw new Exception("Experience doesn't be able to decrease");
    }
}