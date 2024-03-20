using Tools;
using UnityEngine;

public class NPCEntitySpecs : EntitySpecs
{
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public float MinMoveTime;
    [HideInInspector] public float MaxMoveTime;
    [HideInInspector] public float MinWaitTime;
    [HideInInspector] public float MaxWaitTime;

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