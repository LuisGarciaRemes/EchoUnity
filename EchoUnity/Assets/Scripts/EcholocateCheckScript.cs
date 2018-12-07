using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcholocateCheckScript : MonoBehaviour {

    public GameObject objectToCheck;
    private Anima2D.SpriteMeshInstance mesh;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        if (gameObject.GetComponent<Anima2D.SpriteMeshInstance>() != null)
        {
            mesh = gameObject.GetComponent<Anima2D.SpriteMeshInstance>();
        }
        else
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Anima2D.SpriteMeshInstance>() != null)
        {
            mesh.color = objectToCheck.GetComponent<Anima2D.SpriteMeshInstance>().color;
        }
        else
        {
            sprite.color = objectToCheck.GetComponent<Anima2D.SpriteMeshInstance>().color;
        }
        
    }
}
