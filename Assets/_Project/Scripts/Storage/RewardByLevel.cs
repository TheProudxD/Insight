using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Storage
{
    [CreateAssetMenu(menuName = "Create RewardByLevel", fileName = "RewardByLevel", order = -1)]
    public class RewardByLevel : ScriptableObject
    {
        [SerializeField] private List<LevelRewards> _rewards = new();

        public Dictionary<Scene, LevelRewards> Get
            => _rewards.ToDictionary(i => i.Scene, x => x);

        [Serializable]
        public class LevelRewards
        {
            public Scene Scene;
            public int SoftCurrencyAmount;
            public int HardCurrencyAmount;
            public int EnergyAmount;
        }
    }
}