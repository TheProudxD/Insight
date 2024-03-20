using Tools;
using UnityEngine;

public class PlayerEntitySpecs : EntitySpecs
{
    [HideInInspector] public float Hp;
    [HideInInspector] public float BaseAttack;
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public float AttackRadius;
    [HideInInspector] public float AttackCooldown;
    [HideInInspector] public float ExperienceAmount;
    [HideInInspector] public float ManaAmount;
    [HideInInspector] public float ManaRecoverySpeed;
    [HideInInspector] public float TimeAfterAttackManaIncrease;

    public override void Initialize(string[] cells)
    {
        Id = cells[0];
        Hp = TypeParser.ParseFloat(cells[1]);
        BaseAttack = TypeParser.ParseFloat(cells[2]);
        AttackCooldown = TypeParser.ParseFloat(cells[3]);
        MoveSpeed = TypeParser.ParseFloat(cells[4]);
        AttackRadius = TypeParser.ParseFloat(cells[5]);
        ManaAmount = TypeParser.ParseFloat(cells[6]);
        ManaRecoverySpeed = TypeParser.ParseFloat(cells[7]);
        TimeAfterAttackManaIncrease = TypeParser.ParseFloat(cells[8]);
        ExperienceAmount = TypeParser.ParseFloat(cells[9]);
    }
}
