using System;
using UnityEngine;

namespace Player
{
    public class PlayerExp : PlayerFeature<int>
    {
        [SerializeField] private PlayerExpView _playerExpView;
        public int Level { get; private set; }

        public void Initialize(int currentExpAmount, int currentLevel)
        {
            Amount = currentExpAmount;
            Level = currentLevel;
            MaxAmount = PlayerEntitySpecs.ExperienceAmount;

            _playerExpView.Initialize(MaxAmount, Amount, Level);
            Signal.Raise(-MaxAmount);
        }

        public override bool TryIncrease(int amount)
        {
            if (Math.Abs(Amount - MaxAmount) < 0.01f)
                return false;

            Signal.Raise(-amount);

            Amount += amount;
            if (Amount > MaxAmount)
            {
                Amount -= MaxAmount;
                Level++;

                _playerExpView.DisplayExp(Amount);
                _playerExpView.DisplayLevel(Level);
                // SAVE LEVEL AND EXP AMOUNT
            }

            return true;
        }

        public override void Decrease(int amount) => throw new Exception("Experience doesn't be able to decrease");
    }
}