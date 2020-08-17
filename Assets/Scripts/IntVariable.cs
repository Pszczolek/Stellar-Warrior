using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Integer Variable")]
public class IntVariable : ScriptableObject {

    public int Value;
    [SerializeField] int startingValue;

    private void OnEnable()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        Value = startingValue;
    }
}
