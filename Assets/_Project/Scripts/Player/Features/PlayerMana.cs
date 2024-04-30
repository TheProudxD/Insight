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

        protected void Awake()
        {
            MaxAmount = PlayerEntitySpecs.ManaAmount;
            Amount = PlayerEntitySpecs.ManaAmount;
            _recoverySpeed = PlayerEntitySpecs.ManaRecoverySpeed;
            _timeAfterAttack = PlayerEntitySpecs.TimeAfterAttackManaIncrease;
            
            Signal.Raise(-MaxAmount);
        }

        private void Update()
        {
            _timerAfterAttack += Time.deltaTime;
            if (_timerAfterAttack < _timeAfterAttack)
                return;

            _timerRecovery += Time.deltaTime;
            if (_timerRecovery > _recoverySpeed && Math.Abs(Amount - MaxAmount) > 0.01f)
            {
                TryIncrease(PlayerEntitySpecs.ManaRecoveryStep);
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