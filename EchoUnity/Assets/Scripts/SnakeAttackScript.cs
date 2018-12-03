using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttackScript : MonoBehaviour {

    public float attackSpeedMin;
    public float attackSpeedMax;
    public GameObject venom;

    private float attackTimer;
    private float attackSpeed;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        attackTimer = 0;
        attackSpeed = Random.Range(attackSpeedMin,attackSpeedMax);
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.Find("GameManager").GetComponent<GameManagerScript>().gameOver) {
            if (attackTimer >= attackSpeed)
            {
                Instantiate(venom, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0.0f), Quaternion.identity);
                attackTimer = 0;
                attackSpeed = Random.Range(attackSpeedMin, attackSpeedMax);
                audioSource.Play();
            }
            else
            {
                attackTimer += Time.deltaTime;
            }
        }
	}
}
