using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameOverScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(GameManagerScript.instance.die)
        {
            Destroy(this.gameObject);
        }
	}
}
