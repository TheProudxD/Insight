using Tools;
using UnityEngine;

public class CoinPowerupEntitySpecs: EntitySpecs
{
    [HideInInspector] public int Amount;
    [HideInInspector] public float DestroyTimeAfterSpawn;

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Amount = TypeParser.ParseInt(cells[1]);
        DestroyTimeAfterSpawn = TypeParser.ParseFloat(cells[2]);
    }
}