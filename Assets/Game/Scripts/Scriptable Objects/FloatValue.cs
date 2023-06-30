using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/FloatValue", fileName = "FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [FormerlySerializedAs("InititialValue")]
    public float _inititialValue;

    [NonSerialized] public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = _inititialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}