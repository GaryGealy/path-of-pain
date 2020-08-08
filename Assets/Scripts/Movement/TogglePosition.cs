#region File Description
//-----------------------------------------------------------------------------
// TogglePosition.cs
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

public class TogglePosition : MonoBehaviour
{
	
	#region enums
	#endregion
	
	#region fields
	bool isActive;
	#endregion
	
	#region properties
	public float speed = 1;

	// cubes are 1x1 and scaled, use this to get
	// movement units.
	public float scale = 1;

	public Vector3 startPosition;
	public Vector3 togglePosition;

	public Vector3 posMoveTowards;

	public MoveType moveType;
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

	}
	
#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() {
		isActive = false;

		startPosition = transform.position;

		Vector3 toggleDirection;

		switch (moveType) {
			case MoveType.up:
				toggleDirection = Vector3.up;
				break;
			case MoveType.down:
				toggleDirection = Vector3.down;
				break;
			case MoveType.left:
				toggleDirection = Vector3.left;
				break;
			case MoveType.right:
				toggleDirection = Vector3.right;
				break;
			default:
				toggleDirection = Vector3.left;
				break;
		}

		togglePosition  = startPosition + (toggleDirection * scale);
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() {
	}
	#endregion
	
	#region methods
	// Update is called once per frame
	void Update() {
	}

	IEnumerator Coroutine() 
	{
		while ( isActive ) 
		{
			
	        float step = speed * Time.deltaTime;
	        transform.position = Vector3.MoveTowards(transform.position, posMoveTowards, step);

			yield return 0;
		}
	}
	
	public void Toggle() {
		isActive = true;

		if ( startPosition == transform.position )
		{
			posMoveTowards = togglePosition;
		}
		else 
		{
			posMoveTowards = startPosition;
		}

		StartCoroutine(Coroutine());
	}
	
	#endregion
	
}
