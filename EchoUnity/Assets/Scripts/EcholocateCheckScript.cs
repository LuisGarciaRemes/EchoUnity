using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcholocateCheckScript : MonoBehaviour {

    public GameObject objectToCheck;
    private Anima2D.SpriteMeshInstance mesh;

	// Use this for initialization
	void Start () {
        mesh = gameObject.GetComponent<Anima2D.SpriteMeshInstance>();
	}
	
	// Update is called once per frame
	void Update () {
        mesh.color = objectToCheck.GetComponent<Anima2D.SpriteMeshInstance>().color;
    }
}
