using System;
using Storage;
using Tools;

public class RewardByLevelSpecs : EntitySpecs
{
    public Scene Scene;
    public int SoftCurrencyAmount;
    public int HardCurrencyAmount;
    public int EnergyAmount;

    public override void Initialize(string[] cells)
    {
        Id = cells[0];

        Scene = Enum.Parse<Scene>(Id);
        SoftCurrencyAmount = TypeParser.ParseInt(cells[1]);
        HardCurrencyAmount = TypeParser.ParseInt(cells[2]);
        EnergyAmount = TypeParser.ParseInt(cells[3]);
    }
}