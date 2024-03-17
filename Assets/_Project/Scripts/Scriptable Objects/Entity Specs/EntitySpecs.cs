using Tools;
using UnityEngine;

public abstract class EntitySpecs : ScriptableObject
{
	[HideInInspector] public string Id;

    public override string ToString() => this.GiveAllFields();

    public abstract void Initialize(string[] cells);
}