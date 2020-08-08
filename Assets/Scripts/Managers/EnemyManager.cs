#region File Description
//-----------------------------------------------------------------------------
// EnemyNanager.cs
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

using App.Classes;
using Game.Enums;
#endregion

public class EnemyManager : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;
#endregion
	
#region properties
    public GameObject EnemyPrefab;
    public GameObject EnemyParent;
	public GameObject EnemyTarget;

	public Vector3 SpawnOffset;
	public float SpawnRate;

	public float minSpeed;
	public float maxSpeed;
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
        InvokeRepeating("CreateRepeat", 1.0f, SpawnRate);
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
		CreateEnemy( EnemyParent, EnemyPrefab);
	}

    void CreateEnemy(GameObject parent, GameObject prefab)
    {
        GameObject newEnemy;
		Vector3 nextPos;

        nextPos = EnemyTarget.transform.position + SpawnOffset;
        newEnemy = Instantiate(prefab, nextPos, Quaternion.identity);
		newEnemy.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 359.0f));
	    newEnemy.transform.SetParent(parent.transform);

		newEnemy.GetComponentInChildren<EnemyHandler>().SetSpeedRange( minSpeed, maxSpeed );
		newEnemy.GetComponentInChildren<EnemyHandler>().Activate( EnemyTarget );
		
		ColorEntry nextColor = GetNextColor();
		newEnemy.GetComponentInChildren<SpriteRenderer>().material.color = nextColor.color;
	}

	ColorEntry GetNextColor() 
	{
		int enumCount = System.Enum.GetNames(typeof(ElementName)).Length;
		int index = Random.Range(0, enumCount);

		ColorEntry nextColor = activeAppManager.GetComponent<AppManager>().GetElementColor( index );

		return( nextColor );
	}

	public Vector3 GetSpawnOffset()  
	{
		return SpawnOffset;
	}
	
	public void Explode( Vector2 hitPoint)
	{
		//flatFx.AddEffect(hitPoint, 1);
		//EventManager.DebugLog("Explode()", "");
	}

#endregion
}
