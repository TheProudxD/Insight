using UnityEngine;

namespace _Project.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/IntValue", fileName = "IntValue")]
    public class IntValue : ScriptableObject, ISerializationCallbackReceiver
    {
        [field: SerializeField,Range(0,9)] public int InitialValue { get; private set; }

        [HideInInspector] public int RuntimeValue;

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitialValue;
        }

        public void OnBeforeSerialize()
        {
        }
    }
}