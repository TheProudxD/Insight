using Tools;

public class LogEntitySpecs : EntitySpecs
{
    public float Hp { get; private set; }
    public float MoveSpeed { get; private set; }
    public float AttackRadius { get; private set; }
    public float ChaseRadius { get; private set; }
    public float DestroyTimeAfterDying { get; private set; }

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Hp = TypeParser.ParseFloat(cells[1]);
        MoveSpeed = TypeParser.ParseFloat(cells[2]);
        AttackRadius = TypeParser.ParseFloat(cells[3]);
        ChaseRadius = TypeParser.ParseFloat(cells[4]);
    }
}