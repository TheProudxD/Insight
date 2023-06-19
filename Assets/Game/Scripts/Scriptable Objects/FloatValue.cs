using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FloatValue", fileName = "FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float InititialValue;
    [NonSerialized] public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InititialValue;
    }

    public void OnBeforeSerialize() { }
}
