using Tools;
using UnityEngine;

public class NPCEntitySpecs : EntitySpecs
{
    public float MoveSpeed { get; private set; }
    public float MinMoveTime { get; private set; }
    public float MaxMoveTime { get; private set; }
    public float MinWaitTime { get; private set; }
    public float MaxWaitTime { get; private set; }

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        MoveSpeed = TypeParser.ParseFloat(cells[1]);
        MinMoveTime = TypeParser.ParseFloat(cells[2]);
        MaxMoveTime = TypeParser.ParseFloat(cells[3]);
        MinWaitTime = TypeParser.ParseFloat(cells[4]);
        MaxWaitTime = TypeParser.ParseFloat(cells[5]);
    }
}