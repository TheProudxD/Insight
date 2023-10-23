using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Signal")]
public class Signal : ScriptableObject
{
    private readonly List<WeakReference> _listeners = new();

    public void Raise()
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            if (_listeners[i] != null)
            {
                if (_listeners[i].IsAlive)
                ((SignalListener)_listeners[i].Target)?.OnSingleRaised();
            }
            else
            {
                _listeners.RemoveAt(i);
            }
    }

    public void Raise(float amount)
    {
        for (var i = _listeners.Count - 1; i >= 0; i--)
            if (_listeners[i] != null)
                if (_listeners[i] != null)
                {
                    if (_listeners[i].IsAlive)
                        ((SignalListener)_listeners[i].Target)?.OnSingleRaised(amount);
                }
                else
                {
                    _listeners.RemoveAt(i);
                }
    }

    public void RegisterListener(SignalListener listener)
    {
        _listeners.Add(new WeakReference(listener));
    }

    public void DeRegisterListener(SignalListener listener)
    {
        _listeners.Remove(new WeakReference(listener));
    }
}