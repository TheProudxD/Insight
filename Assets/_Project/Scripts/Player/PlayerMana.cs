using System;
using UnityEngine;

namespace Player
{
    public class PlayerMana : PlayerFeature<float>
    {
        private float _recoverySpeed;
        private float _timeAfterAttack;
        
        private float _timerAfterAttack;
        private float _timerRecovery;
        
        public override float Amount
        {
            get => PlayerEntitySpecs.ManaAmount;
            protected set => PlayerEntitySpecs.ManaAmount = value;
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            MaxAmount = PlayerEntitySpecs.ManaAmount;
            _recoverySpeed = PlayerEntitySpecs.ManaRecoverySpeed;
            _timeAfterAttack = PlayerEntitySpecs.TimeAfterAttackManaIncrease;
        }

        private void Update()
        {
            _timerAfterAttack += Time.deltaTime;
            if (_timerAfterAttack < _timeAfterAttack)
                return;

            _timerRecovery += Time.deltaTime;
            if (_timerRecovery > _recoverySpeed && Math.Abs(Amount - MaxAmount) > 0.01f)
            {
                TryIncrease(0.25f);
                _timerRecovery -= _recoverySpeed;
            }
        }

        public override bool TryIncrease(float amount)
        {
            Signal.Raise(-amount);

            Amount += amount;
            Amount = Mathf.Clamp(Amount, 0, MaxAmount);
            return true;
        }

        public override void Decrease(float amount)
        {
            if (Amount <= 0)
                return;

            _timerAfterAttack = 0;
            _timerRecovery = 0;
            Signal.Raise(amount);
            Amount -= amount;
        }
    }
}