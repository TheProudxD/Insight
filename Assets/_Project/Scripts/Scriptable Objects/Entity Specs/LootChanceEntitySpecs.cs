using Objects.Powerups;
using Tools;
using UnityEngine;

public class LootChanceEntitySpecs: EntitySpecs
{
    public float Amount{ get; private set; }
    public Powerup _powerup { get; private set; }

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Amount = TypeParser.ParseInt(cells[1]);
        //_powerup
    }
}