using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Signal")]
public class Signal : ScriptableObject
{
    private List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
                listeners[i].OnSingleRaised();
        }
    }

    public void Raise(float amount)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null)
                listeners[i].OnSingleRaised(amount);
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
