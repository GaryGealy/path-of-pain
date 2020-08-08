#region File Description
//-----------------------------------------------------------------------------
// PingPongControl.cs
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

public class PingPongMove : MonoBehaviour
{
	
	#region enums
	public enum StartingDirection {leftFirst  = -1, rightFirst = 1}
	#endregion
	
	#region fields
	bool isActive;
	#endregion
	
	#region properties
	public float speed;
	public int durationTicks;
	public float magnitude;
	public bool keepMoving;

	public StartingDirection firstMovement;

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
		StartPingPong();
	}
	
#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() {
		isActive = false;
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() {
	}
	#endregion
	
	#region methods
	// Update is called once per frame
	void Update() {
	}
	

	int ticks;
	Vector3 savePos;

	IEnumerator Coroutine() 
	{

		while ( isActive ) 
		{
			ticks++;

			if ( ticks > durationTicks && !keepMoving)
			{
				isActive = false;
				transform.position = savePos;
			}


			transform.position  += new Vector3 ((int)firstMovement * Mathf.Sin(Time.time * speed) * magnitude, 0, 0);

			yield return 0;
		}
	}
	
	public void StartPingPong() {
		isActive = true;
		ticks = 1;
		savePos = transform.position;
		StartCoroutine(Coroutine());
	}
	
	#endregion
	
}
