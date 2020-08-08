#region File Description
//-----------------------------------------------------------------------------
// TimerManager.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

public class TimerManager : MonoBehaviour {
	
#region enums
#endregion
	
#region fields
	GameObject activeEventManager;
	GameObject activeAppManager;

	float elapsedTime = 0.0f;		
	float countdownTime = 0.0f;	

	bool timerRunning = false;
	
#endregion
	
#region properties
	public GameObject elapsedTimer;
#endregion
	
#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStop += LevelStopEvent;
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnStartTimer += StartTimerEvent;
		EventManager.OnEndRound += EndRoundEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStop -= LevelStopEvent;
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnStartTimer -= StartTimerEvent;
		EventManager.OnEndRound -= EndRoundEvent;
	}

	void LevelStopEvent() 
	{
		timerRunning = false;
	}

	void LevelStartEvent() 
	{
		
	}

	void StartTimerEvent() 
	{
		timerRunning = true;
	}

	void EndRoundEvent(EndRoundInfoEventArgs e)
	{	
		timerRunning = false;
	}

#endregion
		
#region Initialize
	// Use this for initialization
	
	public void Init() 
	{
		// do things here that need to happen after the object is instantiated. 
	}
	
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{

	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() 
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

		elapsedTime = 0;
		elapsedTimer.GetComponentInChildren<Text>().text = GetTimeStr(elapsedTime);
  
	}
#endregion


#region methods

	// Update is called once per frame
	void Update()
	 {
		if ( timerRunning ) 
		{
			elapsedTimer.GetComponentInChildren<Text>().text = GetTimeStr(elapsedTime);
		}
	}
	
	void FixedUpdate() 
	{

		if ( timerRunning ) 
		{
			elapsedTime += Time.deltaTime;

			Singleton.levelTimeScore = elapsedTime;
		}
	}
#endregion


#region Timer

	public float GetTime( float timeValue ) 
	{
		// return seconds, 1.3 , 10.4, etc..
		return (float)System.Math.Round(timeValue, 2);
	}

	public float GetElapsedTime() 
	{
		return GetTime(elapsedTime);
	}

	public string GetTimeStr( float timeValue ) 
	{
		string niceTime = Singleton.TimeFormatForDisplay(timeValue);

		return(niceTime);
	}
#endregion

}
