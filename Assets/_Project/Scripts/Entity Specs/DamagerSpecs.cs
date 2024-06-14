using Tools;

public class DamagerSpecs : EntitySpecs
{
    public float Thrust;
    public float KnockTime;
    public float Damage;
    public float AttackCooldown;
    
    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Thrust = TypeParser.ParseFloat(cells[1]);
        KnockTime = TypeParser.ParseFloat(cells[2]);
        Damage = TypeParser.ParseFloat(cells[3]);
        AttackCooldown = TypeParser.ParseFloat(cells[4]);
    }
}