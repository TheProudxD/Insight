using System;
using Objects.Powerups;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    [SerializeField] private Loot[] Loots;

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