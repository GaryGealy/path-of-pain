#region File Description
//-----------------------------------------------------------------------------
// ScoreManger_Main.cs
//
// Copyright (C) Allegro Interactive Games. All rights reserved.
//-----------------------------------------------------------------------------
//
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

public class ScoreManager_Main : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;
	GameObject activeAudioManager;

	public GameObject highScoreText;
#endregion
	
#region properties
#endregion
	
	
#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop +=LevelStopEvent;
		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -=LevelStopEvent;
		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;
	}

	void LevelStartEvent() 
	{
		isActive = true;
	}

	void LevelStopEvent() 
	{
		isActive = false;
	}
	
	void SceneLoadCompleteEvent() 
	{
		isActive = true;
	}
	
#endregion
		
#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		activeEventManager = GameObject.Find("EventManager");
		if ( !activeEventManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'EventManager' reporting object: " + transform.name);
		}
		
		activeAppManager = GameObject.Find("AppManager");
		if ( !activeAppManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'AppManager' reporting object: " + transform.name);
		}

		activeAudioManager = GameObject.Find("AudioManager");
		if ( !activeAppManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'AudioManager' reporting object: " + transform.name);
		}

		if ( highScoreText )
		{
			highScoreText.SetActive(false);
		}
		else
		{
			EventManager.DebugLog("Start()", "unable to find 'highScoreText' reporting object: " + transform.name);
		}
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() 
	{
	}
#endregion
	
#region methods
	// Update is called once per frame
	void Update()
	{
		if ( isActive) 
		{
		}
	}

	public void CelebrateHighScore( string buttonName ) 
	{
		GameObject button;

		button = GameObject.Find(buttonName);

		if ( button )
		{
			
			if ( highScoreText )
			{
				highScoreText.SetActive(true);
				Animator animator = highScoreText.GetComponent<Animator>();
				if ( animator )
				{
					animator.Play("NewHighScore", -1, 0f);
				}
				else
				{
					EventManager.DebugLog("CelebrateHighScore()", "Animation object not found, reporting object: " + transform.name);
				}
			}
			else 
			{
				EventManager.DebugLog("CelebrateHighScore()", "unable to find 'highScoreText' reporting object: " + transform.name);
			}
		}

	}

	public void ResetHighScoreCelebration() 
	{
		Animator animator = highScoreText.GetComponent<Animator>();
		if ( animator )
		{
			animator.Play("NewHighScore_idle", -1, 0f);
			highScoreText.SetActive(false);
		}
		else
		{
			EventManager.DebugLog("ResetHighScoreCelebration()", "Animation object not found, reporting object: " + transform.name);
		}
	}

#endregion
}
