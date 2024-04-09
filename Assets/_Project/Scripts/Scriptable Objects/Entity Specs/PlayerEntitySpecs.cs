using Tools;

public class PlayerEntitySpecs : EntitySpecs
{
    public float MoveSpeed { get; private set; }
    public float HpAmount { get; private set; }
    public float ManaAmount { get; private set; }
    public float ManaRecoverySpeed { private set; get; }
    public float TimeAfterAttackManaIncrease { get; private set; }
    public int ExperienceAmount { get; private set; }
    public int ArmorAmount { get; private set; }
    
    public float SwordDamage { get; private set; }
    public float SwordAttackCooldown { get; private set; }
    public float SwordAttackDuration { get; private set; }
    public float SwordAttackRadius { get; private set; }

    public float BowDamage { get; private set; }
    public float BowAttackCooldown { get; private set; }
    public float BowAttackDuration { get; private set; }
    
    public override void Initialize(string[] cells)
    {
        Id = cells[0];

        MoveSpeed = TypeParser.ParseFloat(cells[1]);
        HpAmount = TypeParser.ParseFloat(cells[2]);
        ManaAmount = TypeParser.ParseFloat(cells[3]);
        ManaRecoverySpeed = TypeParser.ParseFloat(cells[4]);
        TimeAfterAttackManaIncrease = TypeParser.ParseFloat(cells[5]);
        ArmorAmount = TypeParser.ParseInt(cells[6]);
        ExperienceAmount = TypeParser.ParseInt(cells[7]);
        
        SwordDamage = TypeParser.ParseFloat(cells[8]);
        SwordAttackCooldown = TypeParser.ParseFloat(cells[9]);
        SwordAttackDuration = TypeParser.ParseFloat(cells[10]);
        SwordAttackRadius = TypeParser.ParseFloat(cells[11]);
        
        BowDamage = TypeParser.ParseFloat(cells[12]);
        BowAttackCooldown = TypeParser.ParseFloat(cells[13]);
        BowAttackDuration = TypeParser.ParseFloat(cells[14]);
    }
}