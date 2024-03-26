using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class SignalListener : MonoBehaviour
    {
        [SerializeField] private Signal _signal;
        [SerializeField] private UnityEvent _signalEvent;
        [SerializeField] private UnityEvent<float> _signalEventFloat;

        private void OnEnable() => _signal.RegisterListener(this);

        private void OnDisable() => _signal.DeRegisterListener(this);

        public void SingleRaise() => _signalEvent.Invoke();

        public void SingleRaise(float amount) => _signalEventFloat.Invoke(amount);
    }
}