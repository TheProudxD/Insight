using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    [SerializeField] private Signal _signal;
    [SerializeField] private UnityEvent _signalEvent;
    [SerializeField] private UnityEvent<float> _signalEventInt;

    private void OnEnable()
    {
        _signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        _signal.DeRegisterListener(this);
    }

    public void OnSingleRaised()
    {
        _signalEvent.Invoke();
    }

    public void OnSingleRaised(float amount)
    {
        _signalEventInt.Invoke(amount);
    }
}