using Objects.Powerups;
using Tools;
using UnityEngine;

public class LootChanceEntitySpecs: EntitySpecs
{
    [HideInInspector] public float Amount;
    [HideInInspector] public Powerup _powerup;

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Amount = TypeParser.ParseInt(cells[1]);
        //_powerup
    }
}