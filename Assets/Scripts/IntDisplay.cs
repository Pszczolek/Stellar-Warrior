using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntDisplay : MonoBehaviour {

    [SerializeField] Text myText;
    [SerializeField] IntVariable variable;

    // Use this for initialization
    void Start () {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        myText.text = variable.Value.ToString();
    }
}
