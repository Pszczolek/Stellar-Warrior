using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour {

    public Text text;

	public void DisplayObjectInfo(GameObject objectParam)
    {
        text.text = objectParam.ToString();
    }
}
