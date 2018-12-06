using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyTutorial : MonoBehaviour {
    // event functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Catch();
        }
    }

    // private functions
    void Catch()
    {
        TutorialManager._instance.NextStage();
    }

    void Missed()
    {
        TutorialManager.MissedItem();
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -10)
        {
            Missed();
        }
	}
}
