using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Signal")]
public class Signal : ScriptableObject
{
    private List<SignalListener> listeners = new List<SignalListener>();
    private void Raise()
    {
        for (int i = listeners.Count-1; i >=0; i++)
        {
            listeners[i].OnSingleRaised();
        }
    }
    public void RegisterListener( SignalListener listener)
    {
        listeners.Add(listener);
    }
    public void DeRegisterListener( SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
