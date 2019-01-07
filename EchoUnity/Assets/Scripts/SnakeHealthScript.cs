using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHealthScript : MonoBehaviour {

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public AudioClip splatSound;
    public AudioClip hurtSound;
    public float vol;
    public float hurtTime;
    public GameObject snakeHeadSprite;
    public float xPos;
    public int points;

    private float numHearts;
    private const float MAXLIVES = 5;
    private AudioSource audioSource;
    private bool isHurt;
    private float hurtTimer;

    private void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().timerPoints = false;
        isHurt = false;
        hurtTimer = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        numHearts = 5;
    }
    private void Update()
    {
        UpdateLifeBar();
        UpdateColor();
        if(gameObject.transform.position.x <= xPos)
        {
            gameObject.GetComponent<SimpleMovementScript>().speedX = 0;
        }
    }

    public void TakeDamage()
    {
        isHurt = true;
        hurtTimer = hurtTime;
        numHearts--;
            audioSource.PlayOneShot(splatSound, vol);

        if (numHearts > 0)
            {
                audioSource.PlayOneShot(hurtSound, vol);
            }
        else
        {
            GameObject.Find("GameManager").GetComponent<SpawnerScript>().PlayDeathClip();
        }
    }

    private void UpdateLifeBar()
    {
        for (int i = 0; i < MAXLIVES; i++)
        {
            if (i < numHearts && numHearts > 0)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }

        if (numHearts > MAXLIVES)
        {
            numHearts = MAXLIVES;
        }

        if (numHearts <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().AddPoints(points);
            GameObject.Find("GameManager").GetComponent<SpawnerScript>().spawnNewWave = true;
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().timerPoints = true;
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        if (isHurt)
        {
            snakeHeadSprite.GetComponent<Anima2D.SpriteMeshInstance>().color = Color.red;

            if (hurtTimer <= 0)
            {
                snakeHeadSprite.GetComponent<Anima2D.SpriteMeshInstance>().color = Color.white;
                isHurt = false;
            }
            else
            {
                hurtTimer -= Time.deltaTime;
            }
        }
    }
}
