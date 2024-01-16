using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FloatValue", fileName = "FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [field: SerializeField] public float InitialValue { get; private set; }

    [HideInInspector] public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}