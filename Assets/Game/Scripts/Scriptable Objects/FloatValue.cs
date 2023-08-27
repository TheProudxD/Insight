using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/FloatValue", fileName = "FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [FormerlySerializedAs("_inititialValue")] [Range(0,9)] public float InitialValue;

    [NonSerialized] public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}