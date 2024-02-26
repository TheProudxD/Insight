using System.Collections.Generic;
using Tools;
using UnityEngine;

[System.Serializable]
public class EntityData
{
    public List<EntityOptions> EntitiesOptions = new();

    public override string ToString()
    {
        string result = "";
        EntitiesOptions.ForEach(o =>
        {
            result += o.ToString();
        });
        return result;
    }
}

[System.Serializable]
public class EntityOptions
{
    public string Id;
    public float Hp;
    public float Damage;
    public float Speed;

	public override string ToString() => Utils.GiveAllFields(this);
}