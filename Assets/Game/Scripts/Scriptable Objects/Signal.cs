using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Signal")]
public class Signal : ScriptableObject
{
    private readonly List<SignalListener> _listeners = new();

    public void Raise()
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            if (_listeners[i] != null)
                _listeners[i]?.OnSingleRaised();
    }

    public void Raise(float amount)
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            if (_listeners[i] != null)
                _listeners[i]?.OnSingleRaised(amount);
    }

    public void RegisterListener(SignalListener listener)
    {
        _listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        _listeners.Remove(listener);
    }
}