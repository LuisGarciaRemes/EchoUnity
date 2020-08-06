using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileText : MonoBehaviour
{
    public Text textToSwap;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManagerScript.instance.mobileMode)
        {
            textToSwap.text = text;
        }

    }
}
