using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameOverScript : MonoBehaviour {

    private GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		if(gameManager.GetComponent<GameManagerScript>().die)
        {
            Destroy(this.gameObject);
        }
	}
}
