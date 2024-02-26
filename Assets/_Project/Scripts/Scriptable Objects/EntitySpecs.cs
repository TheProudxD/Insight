using Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Entity Specs", fileName = "EntitySpecs", order = 0)]
public class EntitySpecs : ScriptableObject
{
    public string Id;

    public float Hp;
    public float Damage;
    public float MoveSpeed; 
    public float AttackRadius;
    public float ChaseRadius;

	public override string ToString() => Utils.GiveAllFields(this);
}
