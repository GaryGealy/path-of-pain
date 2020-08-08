#region File Description
//-----------------------------------------------------------------------------
// BackgroundScroller.cs
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

public class BackgroundScroller : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
    Vector3 startPosition;
#endregion
	
#region properties
	public float scrollSpeed;
	public float tileSizeZ;
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
		startPosition = transform.position;

		//var renderer = this.transform.GetComponent<SpriteRenderer>();
		//Debug.Log(renderer.bounds);
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
			float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
			//Debug.Log(newPosition);
        	transform.position = startPosition + Vector3.right * newPosition;
		}
	}
#endregion
}
