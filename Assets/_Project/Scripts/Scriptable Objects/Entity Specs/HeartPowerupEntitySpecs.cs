using Tools;
using UnityEngine;

public class HeartPowerupEntitySpecs: EntitySpecs
{
    public float HealAmount { get; private set; }
    public float DestroyTimeAfterSpawn { get; private set; }

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        HealAmount = TypeParser.ParseFloat(cells[1]);
        DestroyTimeAfterSpawn = TypeParser.ParseFloat(cells[2]);
    }
}