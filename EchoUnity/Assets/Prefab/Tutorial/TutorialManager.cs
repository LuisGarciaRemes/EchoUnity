﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    // Data classes
    enum TutorialStage {
        MovePause = 0,
        MoveTry = 1,
        AvoidObstaclePause = 2,
        AvoidObstacleTry = 3,
        CatchItemPause = 4,
        CatchItemTry = 5,
        TutorialEnding = 6,
        Default = -1
    }

    // GUI editable variables
    public float moveTryTime = 1;
    public float avoidPauseTime = 3;
    public float catchPauseTime = 3;
    public float finalPauseTime = 3;

    public GameObject tutorialObstacle;
    public GameObject tutorialItem;
    public Joystick joystick;
    public FloatingButtonScript floatingButton;

    // private variables
    TutorialStage _stage;
    float _lastCheckRealTime = -1;

    // singleton instance
    public static TutorialManager _instance;

    // public functions
    public void _StartTutorial()
    {
        GameManagerScript.instance.NoUIPause();
        _stage = TutorialStage.MovePause;
        Time.timeScale = 0;
        _lastCheckRealTime = Time.realtimeSinceStartup;
        TutorialCanvas.DisplayText(0);
    }

    public static void StartTutorial()
    {
        if (_instance)
        {
            _instance._StartTutorial();
        }
    }
    
    public void _DisableTutorial()
    {
        _stage = TutorialStage.Default;
    }

    public static void DisableTutorial()
    {
        if (_instance)
        {
            _instance._DisableTutorial();
        }
    }

    public void _HitByObstacle()
    {
        if (tutorialObstacle)
        {
            var obstacle =
                Instantiate(
                    tutorialObstacle,
                    new Vector3(100, 0, 0),
                    Quaternion.Euler(0, 0, 0));
        }
    }

    public static void HitByObstacle()
    {
        _instance._HitByObstacle();
    }

    public void _MissedItem()
    {
        if (tutorialItem)
        {
            var item = Instantiate(
                    tutorialItem,
                    new Vector3(100, 0, 0),
                    Quaternion.Euler(0, 0, 0));
        }
    }

    public static void MissedItem()
    {
        _instance._MissedItem();
    }

    // private functions
    public void NextStage()
    {
        Debug.Log(_stage);
        if(_stage != TutorialStage.Default)
        {
            _stage++;
            if(_stage == TutorialStage.MoveTry)
            {
                GameManagerScript.instance.NoUIResume();
                Time.timeScale = 1.0f;
                //StartCoroutine("EndMoveTry", moveTryTime);
                _lastCheckRealTime = Time.realtimeSinceStartup;
            }else if(_stage == TutorialStage.AvoidObstaclePause)
            {
                GameManagerScript.instance.NoUIPause();
                Time.timeScale = 0.0f;
                _lastCheckRealTime = Time.realtimeSinceStartup;
                TutorialCanvas.DisplayText(1);
            }else if(_stage == TutorialStage.AvoidObstacleTry)
            {
                GameManagerScript.instance.NoUIResume();
                Time.timeScale = 1.0f;
                if (tutorialObstacle)
                {
                    var obstacle = 
                        Instantiate(
                            tutorialObstacle, 
                            new Vector3(100, 0, 0), 
                            Quaternion.Euler(0, 0, 0));
                }
            }else if(_stage == TutorialStage.CatchItemPause)
            {
                GameManagerScript.instance.NoUIPause();
                Time.timeScale = 0.0f;
                _lastCheckRealTime = Time.realtimeSinceStartup;
                TutorialCanvas.DisplayText(2);
            }
            else if(_stage == TutorialStage.CatchItemTry)
            {
                GameManagerScript.instance.NoUIResume();
                Time.timeScale = 1.0f;
                if (tutorialItem)
                {
                    var item = Instantiate(
                            tutorialItem,
                            new Vector3(100, 0, 0),
                            Quaternion.Euler(0, 0, 0));
                }
            }
            else if(_stage == TutorialStage.TutorialEnding)
            {
                //GameManagerScript.instance.NoUIPause();
                //Time.timeScale = 0.0f;
                _lastCheckRealTime = Time.realtimeSinceStartup;
                TutorialCanvas.DisplayText(3);
            }
        }
    }

    IEnumerator EndMoveTry()
    {
        yield return new WaitForSecondsRealtime(moveTryTime);
        if (_stage == TutorialStage.MoveTry)
        {
            NextStage();
        }
    }

    // Use this for initialization
    private void Awake()
    {
        _instance = this;
    }

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var next = false;
		// Tutorial Procedural Controll
        if(_stage == TutorialStage.MovePause)
        {
            if(GameManagerScript.instance.mobileMode){
                if(joystick.Vertical != 0 || joystick.Horizontal != 0){
                    next = true;
                }
            } else if(Input.GetAxis("Vertical") > 0||
               Input.GetAxis("Vertical") < 0 ||
                Input.GetAxis("Horizontal") > 0 || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || 
                Input.GetAxis("Horizontal") < 0 || Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                next = true;
            }
            if(next){
                NextStage();
            }
        }else if(_stage == TutorialStage.MoveTry)
        {
            if(GameManagerScript.instance.mobileMode){
                if(floatingButton.GetInput()){
                    next = true;
                }
            } else if (Input.GetButtonDown("Echo"))
            {
                next = true;
            }
            if(next){
                StartCoroutine("EndMoveTry");
            }
            //if(Time.realtimeSinceStartup - _lastCheckRealTime > moveTryTime)
            //{
            //    NextStage();
            //}
        }else if(_stage == TutorialStage.AvoidObstaclePause)
        {
            if(Time.realtimeSinceStartup - _lastCheckRealTime > avoidPauseTime)
            {
                NextStage();
            }
        }else if(_stage == TutorialStage.AvoidObstacleTry)
        {
            
        }else if(_stage == TutorialStage.CatchItemPause)
        {
            if (Time.realtimeSinceStartup - _lastCheckRealTime > catchPauseTime)
            {
                NextStage();
            }
        }
        else if(_stage == TutorialStage.CatchItemTry)
        {

        }else if(_stage == TutorialStage.TutorialEnding)
        {
            if (Time.realtimeSinceStartup - _lastCheckRealTime > finalPauseTime)
            {
                joystick.gameObject.SetActive(false);
                floatingButton.gameObject.SetActive(false);
                TutorialCanvas.HideAllTexts();
                _stage = TutorialStage.Default;
                GameManagerScript.instance.ToStartScreen();
            }
        }
	}
}
