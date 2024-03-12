using UnityEngine;

namespace Player
{
    public abstract class PlayerFeature<T> : MonoBehaviour
    {
        [SerializeField] protected FloatValue CurrentValue;
        [SerializeField] protected Signal Signal;

        public float Value
        {
            get => CurrentValue.RuntimeValue;
            protected set => CurrentValue.RuntimeValue = value;
        }
        
        public abstract bool TryIncrease(T value);
        
        public abstract void Decrease(T value);
        
        protected float MaxValue => CurrentValue.InitialValue;

        protected virtual void Awake() => Signal.Raise(-MaxValue);
    }
}