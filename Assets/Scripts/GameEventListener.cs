using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ObjectEvent : UnityEvent<GameObject> { }
[Serializable]
public class IntEvent: UnityEvent<int> { }

public class GameEventListener : MonoBehaviour {

    public GameEvent gameEvent;
    public UnityEvent response;
    public ObjectEvent responseWithParam;
    public IntEvent responseWithInt;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }

    public void OnEventRaised(GameObject objectParam)
    {
        responseWithParam.Invoke(objectParam);
    }

    public void OnEventRaised(int intParam)
    {
        responseWithInt.Invoke(intParam);
    }
}
