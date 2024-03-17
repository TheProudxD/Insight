using Tools;
using UnityEngine;

public class LogEntitySpecs : EntitySpecs
{
    [HideInInspector] public float Hp;
    [HideInInspector] public float Damage;
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public float AttackRadius;
    [HideInInspector] public float ChaseRadius;

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Hp = TypeParser.ParseFloat(cells[1]);
        Damage = TypeParser.ParseFloat(cells[2]);
        MoveSpeed = TypeParser.ParseFloat(cells[3]);
        AttackRadius = TypeParser.ParseFloat(cells[4]);
        ChaseRadius = TypeParser.ParseFloat(cells[5]);
    }
}