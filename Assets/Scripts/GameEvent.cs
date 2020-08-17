using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject {
    [SerializeField] protected List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        Debug.Log(name + " listeners: " + listeners);
        for (int i = listeners.Count - 1; i>=0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void Raise(GameObject objectParam)
    {
        Debug.Log(name + " listeners: " + listeners);
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(objectParam);
        }
    }

    public void Raise(int intParam)
    {
        Debug.Log(name + " listeners: " + listeners);
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(intParam);
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
        //Debug.Log(listener + " registered as listener for " + name);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
        //Debug.Log(listener + " unregistered as listener for " + name);
    }
    
}
