using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerNameScript : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip highScoreSound;
    public GameObject enterButton;
    public InputField text;
    public GameObject displaytext;

    private void Awake()
    {     
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        enterButton.SetActive(true);
        audioSource.PlayOneShot(highScoreSound);
        text.gameObject.SetActive(true);
        displaytext.SetActive(true);
    }

    private void OnDisable()
    {
        if (enterButton.activeSelf)
        {
            enterButton.SetActive(false);
        }
        text.gameObject.SetActive(false);
        displaytext.SetActive(false);
    }
}
