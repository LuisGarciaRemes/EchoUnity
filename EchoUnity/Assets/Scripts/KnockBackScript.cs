using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackScript : MonoBehaviour {

    public float knockBackAmount;

    private GameObject echo;
    private AudioSource audioSource;
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        echo = GameObject.Find("Echo");
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            if (echo.GetComponent<PlayerScript>().canTakeDamage) {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !gameManager.GetComponent<GameManagerScript>().paused)
        {
            float direction;

            if(echo.transform.position.y >= gameObject.transform.position.y)
            {
                direction = -.25f;
            }
            else
            {
                direction = .25f;
            }
                echo.GetComponent<Rigidbody2D>().transform.position -= new Vector3(knockBackAmount, knockBackAmount*direction, 0f);
        }
    }
}
