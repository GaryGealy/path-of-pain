#region File Description
//-----------------------------------------------------------------------------
// ProjectileManager.cs
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
using App.Classes;

#endregion

public class ProjectileManager : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;
	
	Dictionary<string, Color> colorDict;
	[SerializeField] FlatFX flatFx;
#endregion
	
#region properties
    public GameObject bulletPrefab;
	public GameObject projectileParent;
	public GameObject bulletSpawnPoint;
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
        //InvokeRepeating("CreateRepeat", 2.0f, 0.3f);
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

		flatFx=GameObject.Find("FlatFX").GetComponent<FlatFX>();
		if ( !flatFx ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'flatFx' reporting object: " + transform.name);
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

	void CreateRepeat() 
	{
		CreateProjectile( bulletPrefab);
	}

    void CreateProjectile( GameObject prefab ) 
	{
		GameObject newProjectile;
  		
		//Quaternion rotation = Quaternion.LookRotation(transform.rotation, Vector3.up);

		//transform.parent.gameObject.transform.rotation

		newProjectile = Instantiate(prefab, bulletSpawnPoint.transform.position, transform.rotation );
		newProjectile.transform.SetParent( projectileParent.transform);
		newProjectile.GetComponent<ProjectileHandler>().Activate();

		//newProjectile.GetComponent<BorderBlockHandler>().Id = GameControl.GetNextBorderId();
	}
	
	public void Fire() 
	{
		int effectNumber = 1;

		flatFx.settings[effectNumber].start.innerColor = Color.green;
		flatFx.AddEffect((Vector2)transform.position, effectNumber);
	}
#endregion
}
