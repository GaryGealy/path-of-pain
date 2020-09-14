﻿#region File Description
//-----------------------------------------------------------------------------
// MoveOnAxis.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

public class MoveOnAxis : MonoBehaviour {
	
	#region enums
	#endregion
	
#region fields
		bool isActive;
		bool moveActive;

		GameObject activeEventManager;
		GameObject activeAppManager;
#endregion
	
#region properties
		public float movementSpeed;
	#endregion
	
	
#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop +=LevelStopEvent;
		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;

		EventManager.OnBeginRound += BeginRoundEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -=LevelStopEvent;
		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;

		EventManager.OnBeginRound -= BeginRoundEvent;
	}

	void LevelStartEvent() 
	{
		isActive = true;
		moveActive = true;
	}

	void LevelStopEvent() 
	{
		isActive = false;
		moveActive = false;
	}
	
	void SceneLoadCompleteEvent() 
	{
		isActive = true;
	}

	void BeginRoundEvent()
	{	
		moveActive = true;
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

	}
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() 
	{
		isActive = true;
		moveActive = true;
	}
#endregion
	
#region methods
	// Update is called once per frame
	void Update() 
	{	
		if ( isActive )
		{
			if ( moveActive )
			{
				transform.position += -Vector3.forward * Time.deltaTime * movementSpeed;
			}
		}
	}

	public void ToggleActiveMovement( float speed ) 
	{
		movementSpeed = speed;
		moveActive = !moveActive;
	}

	public void ToggleActive() 
	{
		isActive = !isActive;
	}

	#endregion
	
}
