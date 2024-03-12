using System;
using Tools;
using UnityEngine;

public abstract class EntitySpecs : ScriptableObject
{
	[HideInInspector] public string Id;
    protected enum RowTypes
    {
        Id = 0,
        Hp,
        Damage,
        MoveSpeed,
        AttackRadius,
        ChaseRadius,
    }

    public override string ToString() => this.GiveAllFields();

    public abstract void Initialize(string[] cells);
}