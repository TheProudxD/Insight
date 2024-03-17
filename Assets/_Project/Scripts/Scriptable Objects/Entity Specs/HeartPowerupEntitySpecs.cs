using Tools;
using UnityEngine;

public class HeartPowerupEntitySpecs: EntitySpecs
{
    [HideInInspector] public float HealAmount;
    [HideInInspector] public float DestroyTimeAfterSpawn;

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        HealAmount = TypeParser.ParseFloat(cells[1]);
        DestroyTimeAfterSpawn = TypeParser.ParseFloat(cells[2]);
    }
}