#region File Description
//-----------------------------------------------------------------------------
// ColliderHelper.cs
//
// Copyright (C) Allegro Interactive Games. All rights reserved.
//-----------------------------------------------------------------------------
//
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.ParticleSystemModule;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

[RequireComponent(typeof(EnemyHandler))]
public class ColliderHelper : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;
	GameObject activeEnemyManager;
	GameObject activeAudioManager;
#endregion
	
#region properties
	public GameObject CollisionEffectPrefab;
	public Vector3 ExplosionOffset;
	public string ColliderTag;
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

		activeAudioManager  = GameObject.Find("AudioManager");
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

    void OnCollisionEnter2D(Collision2D other)
    {
		if (other.gameObject.tag == "Player") 
		{	

			//EventManager.DebugLog("OnTriggerEnter2D()", other.transform.name);
		
		/*	GameObject explode = Instantiate(CollisionEffectPrefab, other.transform.position, Quaternion.identity);
			explode.transform.SetParent(other.transform);

			ParticleSystem explosion = explode.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
			explosion.startSize = other.transform.lossyScale.magnitude;
			explosion.Play();
		*/
		
			//EventManager.DebugLog("OnCollisionEnter2D()", other.transform.name);

		
		}
		
    }

	void OnTriggerEnter2D (Collider2D other)
	{
		//EventManager.DebugLog("OnTriggerEnter2D()", other.transform.name);
		
		activeEnemyManager.GetComponent<EnemyManager>().Explode(other.transform.position);

		activeAudioManager.GetComponent<AudioManager>().EnemyHit();
		
		Destroy(transform.parent.gameObject);
	}

#endregion
}
