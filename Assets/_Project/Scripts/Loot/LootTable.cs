using System;
using Objects.Powerups;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    [SerializeField] private bool _isLimitedBy100;
    [SerializeField] private Loot[] Loots;

    private void OnValidate()
    {
        if (!_isLimitedBy100) return;

        var sum = 0;
        foreach (var loot in Loots)
        {
            sum += loot.Chance;
        }

        if (sum != 100)
            throw new ArgumentException("Chance sum must be equal 100!");
    }

    public Powerup LootPowerup()
    {
        int cumulativeProb = 0;
        int currentProb = Random.Range(0, 100);
        foreach (var loot in Loots)
        {
            cumulativeProb += loot.Chance;
            if (currentProb <= cumulativeProb)
            {
                return loot.Powerup;
            }
        }

        return null;
    }
}

[Serializable]
public class Loot
{
    public Powerup Powerup;
    public int Chance;
}