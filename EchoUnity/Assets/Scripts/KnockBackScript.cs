using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackScript : MonoBehaviour {

    public float knockBackAmount;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GameManagerScript.instance.paused)
        {
                audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !GameManagerScript.instance.paused)
        {
            float direction;

            if(collision.transform.position.y >= gameObject.transform.position.y)
            {
                direction = -.25f;
            }
            else
            {
                direction = .25f;
            }
            collision.transform.position -= new Vector3(knockBackAmount, knockBackAmount*direction, 0f);
        }
    }
}
