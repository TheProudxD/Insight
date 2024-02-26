using System.Collections.Generic;
using Tools;
using UnityEngine;

[System.Serializable]
public class EntityData
{
    public List<EntitySpecs> EntitiesOptions = new();

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