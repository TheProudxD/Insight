using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/BoolValue", fileName = "BoolValue")]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    [field: SerializeField] public bool InitialValue { get; private set; }

    [HideInInspector] public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}