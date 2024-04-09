using Tools;
using UnityEngine;

public abstract class EntitySpecs
{
    public string Id { get; protected set; }

    public override string ToString() => this.GiveAllFields();

    public abstract void Initialize(string[] cells);
}