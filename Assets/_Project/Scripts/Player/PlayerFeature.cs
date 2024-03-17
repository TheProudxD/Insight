using UnityEngine;
using Zenject;

namespace Player
{
    public abstract class PlayerFeature<T> : MonoBehaviour
    {
        [Inject] protected PlayerEntitySpecs PlayerEntitySpecs;
        [SerializeField] protected Signal Signal;

        public abstract float Amount { get; protected set; }

        public abstract bool TryIncrease(T value);
        public abstract void Decrease(T value);

        protected virtual void Awake() => Signal.Raise(-MaxAmount);
        public float MaxAmount { get; protected set; }
    }
}