using System;
using UnityEngine;

namespace Player
{
    public class PlayerMana : PlayerFeature<float>
    {
        private readonly int _recoverySpeed = 2;
        private readonly int _timeAfterAttack = 8;
        private float _timerAfterAttack;
        private float _timerRecovery;

        public override bool TryIncrease(float amount)
        {
            Signal.Raise(-amount);

            Value += amount;
            Value = Mathf.Clamp(Value, 0, MaxValue);
            return true;
        }

        public override void Decrease(float amount)
        {
            if (Value <= 0)
                return;

            _timerAfterAttack = 0;
            _timerRecovery = 0;
            Signal.Raise(amount);
            Value -= amount;
            print(Value);
        }

        private void Update()
        {
            _timerAfterAttack += Time.deltaTime;
            if (_timerAfterAttack < _timeAfterAttack)
                return;

            _timerRecovery += Time.deltaTime;
            if (_timerRecovery > _recoverySpeed && Math.Abs(Value - MaxValue) > 0.01f)
            {
                TryIncrease(0.25f);
                _timerRecovery -= _recoverySpeed;
            }
        }
    }
}