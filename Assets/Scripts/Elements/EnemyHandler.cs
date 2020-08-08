#region File Description
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

public class EnemyHandler : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	GameObject activeEventManager;
	GameObject activeAppManager;
	GameObject activeEnemyManager;

	GameObject activeTarget;

    bool isActive;
    bool moveActive;

	Vector3 spawnOffset;

    float movementSpeed;

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

		activeEnemyManager = GameObject.Find("EnemyManager");
		if ( !activeEventManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'EnemyManager' reporting object: " + transform.name);
		}

		spawnOffset = activeEnemyManager.GetComponent<EnemyManager>().GetSpawnOffset();
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
		if ( moveActive && isActive )
        {
       		float step =  movementSpeed * Time.deltaTime; // calculate distance to move 
			transform.position = Vector3.MoveTowards(transform.position, activeTarget.transform.position + spawnOffset, step);

			if (Vector3.Distance(transform.position, activeTarget.transform.position) < 0.001f)
			{
			}
		}
	}

	void FixedUpdate() 
    {
    }

	public void SetSpeedRange( float minSpeed, float maxSpeed )
	{
		movementSpeed = Random.Range(minSpeed, maxSpeed);
	}

	public void Activate( GameObject target) 
	{	
		//EventManager.DebugLog("Activate()", target.transform.position.ToString());
		moveActive = true;
		isActive = true;

		activeTarget = target;
	}
#endregion
}
