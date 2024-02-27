using Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Entity Specs", fileName = "EntitySpecs", order = 0)]
public class EntitySpecs : ScriptableObject
{
	[HideInInspector] public string Id;
    [HideInInspector] public float Hp;
    [HideInInspector] public float Damage;
    [HideInInspector] public float MoveSpeed; 
    [HideInInspector] public float AttackRadius;
    [HideInInspector] public float ChaseRadius;

	public override string ToString() => this.GiveAllFields();
}
