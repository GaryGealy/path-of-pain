﻿#region File Description
//-----------------------------------------------------------------------------
// ProjectileHandler.cs
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

public class ProjectileHandler : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	GameObject activeEventManager;
	GameObject activeAppManager;
	
    bool isActive;
    bool moveActive;

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

		 Destroy(gameObject, 5);

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

	void FixedUpdate() 
    {
        if ( moveActive && isActive )
        {
			//transform.rotation = Quaternion.Euler(0, 0, 180);
			Vector3 direction =   transform.up; //transform.rotation * Vector3.forward;
		    transform.position += direction * Time.deltaTime * movementSpeed;
        }

    }

	public void Activate() 
	{	
		//EventManager.DebugLog("CreateProjectile()", transform.rotation.ToString());
		moveActive = true;
		isActive = true;
	}
#endregion
}
