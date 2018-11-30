using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerNameScript : MonoBehaviour {

    internal string stringToEdit;
    private AudioSource audioSource;
    public AudioClip highScoreSound;

    private void Awake()
    {     
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        stringToEdit = " HighScore!!! Please Enter Your Name";
        audioSource.PlayOneShot(highScoreSound);
    }

    void OnGUI()
    {
        stringToEdit = GUI.TextField(new Rect(40, 300, 725, 50), stringToEdit, 50);
        GUI.skin.textField.fontSize = 40;
    }
}
