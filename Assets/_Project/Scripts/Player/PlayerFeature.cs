using UnityEngine;

namespace Player
{
    public abstract class PlayerFeature : MonoBehaviour
    {
        [SerializeField] protected FloatValue CurrentValue;
        [SerializeField] protected Signal Signal;

        public float Value
        {
            get => CurrentValue.RuntimeValue;
            protected set => CurrentValue.RuntimeValue = value;
        }

        protected float MaxValue => CurrentValue.InitialValue;

        protected void Awake() => Signal.Raise(-MaxValue);
    }
}