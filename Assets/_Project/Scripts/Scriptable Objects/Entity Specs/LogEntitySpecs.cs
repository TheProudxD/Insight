using Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Log Entity Specs", fileName = "Log Entity Specs", order = 0)]
public class LogEntitySpecs: EntitySpecs
{
    [HideInInspector] public float Hp;
    [HideInInspector] public float Damage;
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public float AttackRadius;
    [HideInInspector] public float ChaseRadius;

	public override void Initialize(string[] cells)
	{
        var id = cells[(int)RowTypes.Id];
        var hp = TypeParser.ParseFloat(cells[(int)RowTypes.Hp]);
        var damage = TypeParser.ParseFloat(cells[(int)RowTypes.Damage]);
        var speed = TypeParser.ParseFloat(cells[(int)RowTypes.MoveSpeed]);
        var attackRadius = TypeParser.ParseFloat(cells[(int)RowTypes.AttackRadius]);
        var chaseRadius = TypeParser.ParseFloat(cells[(int)RowTypes.ChaseRadius]);

        Id = id;
        Hp = hp;
        Damage = damage;
        MoveSpeed = speed;
        AttackRadius = attackRadius;
        ChaseRadius = chaseRadius;
    }
}