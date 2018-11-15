using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour {

    public float scrollSpeed;
    public float tileSizeX;

    private Vector3 startPosition;

    //Gets the transform position
    void Start()
    {
        startPosition = transform.position;
    }

    //Scrolls the background
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
        transform.position = startPosition + new Vector3(-1.0f,0.0f,0.0f) * newPosition;
    }
}

