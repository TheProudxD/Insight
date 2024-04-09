using UnityEngine;
using Zenject;

namespace Player
{
    public abstract class PlayerFeature<T> : MonoBehaviour 
        where T: struct
    {
        [Inject] protected PlayerEntitySpecs PlayerEntitySpecs;
        [SerializeField] protected Signal Signal;

        public T Amount { get; protected set; }

        public abstract bool TryIncrease(T value);
        public abstract void Decrease(T value);

        public T MaxAmount { get; protected set; }
    }
}