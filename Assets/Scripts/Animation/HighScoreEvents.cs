﻿#region File Description
//-----------------------------------------------------------------------------
// HighScoreEvents.cs
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

public class HighScoreEvents : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;
	GameObject activeScoreManager;
	GameObject activeAudioManager;
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

		activeScoreManager = GameObject.Find("ScoreManager");
		if ( !activeScoreManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'ScoreManager' reporting object: " + transform.name);
		}

		activeAudioManager = GameObject.Find("AudioManager");
		if ( !activeAudioManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'AudioManager' reporting object: " + transform.name);
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

	// DEVNOTE: called from animation config
	public void AudioCelebrateHighScore()
	{
		activeAudioManager.GetComponent<AudioManager>().NewHighScore();
	}

	public void ResetHideHighScore()
	{
		activeScoreManager.GetComponent<ScoreManager_Main>().ResetHighScoreCelebration();
	}
#endregion
}
