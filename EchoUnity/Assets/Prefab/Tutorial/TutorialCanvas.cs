using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvas : MonoBehaviour {
    // singleton instance
    public static TutorialCanvas _instance;

    // GUI editable variables
    public List<GameObject> tutorialTexts = new List<GameObject>();

    // public functions
    public void _HideAllTexts()
    {
        foreach(var t in tutorialTexts)
        {
            t.SetActive(false);
        }
    }

    public static void HideAllTexts()
    {
        _instance._HideAllTexts();
    }

    public void _DisplayText(int index)
    {
        if(index < tutorialTexts.Count)
        {
            foreach(var t in tutorialTexts)
            {
                t.SetActive(false);
            }
            tutorialTexts[index].SetActive(true);
        }
    }

    public static void DisplayText(int index)
    {
        _instance._DisplayText(index);
    }

    // Use this for initialization
    private void Awake()
    {
        _instance = this;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
