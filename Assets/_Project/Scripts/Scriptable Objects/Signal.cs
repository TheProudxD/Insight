using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Signal")]
public class Signal : ScriptableObject
{
    private readonly List<SignalListener> _listeners = new();

    public void Raise()
    {
        foreach (var t in _listeners)
            t.SingleRaise();
    }

    public void Raise(float amount)
    {
        foreach (var t in _listeners)
            t.SingleRaise(amount);
    }

    public void RegisterListener(SignalListener listener) => _listeners.Add(listener);

    public void DeRegisterListener(SignalListener listener) => _listeners.Remove(listener);
}