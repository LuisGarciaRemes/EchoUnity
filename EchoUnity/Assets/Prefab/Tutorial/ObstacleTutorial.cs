using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTutorial : MonoBehaviour {
    // event functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hit();
        }
    }

    // private functions
    void Hit()
    {
        TutorialManager.HitByObstacle();
        Destroy(gameObject);
    }

    void Avoided()
    {
        TutorialManager._instance.NextStage();
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -10)
        {
            Avoided();
        }
	}
}
