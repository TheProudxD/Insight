using Tools;

public class CoinPowerupEntitySpecs : EntitySpecs
{
    public int Amount { get; private set; }
    public float DestroyTimeAfterSpawn { get; private set; }

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Amount = TypeParser.ParseInt(cells[1]);
        DestroyTimeAfterSpawn = TypeParser.ParseFloat(cells[2]);
    }
}