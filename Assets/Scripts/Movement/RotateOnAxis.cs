#region File Description
//-----------------------------------------------------------------------------
// RotateOnAxis.cs
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

public class RotateOnAxis : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
#endregion
	
#region properties
	public float speed = 1;

	public Vector3 rotation; 
	public bool continuousRotation; 

	public Vector3 backTarget;
	public Vector3 frontTarget;  

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
		Rotate();
	}
	
#endregion
		
#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		frontTarget = transform.position + new Vector3(0,0,50);
		backTarget = transform.position - new Vector3(0,0,50);
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

	IEnumerator Coroutine() 
	{
		while ( isActive ) 
		{
			
			float step = speed * Time.deltaTime;
				Vector3 targetDir = backTarget - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);

			Debug.DrawRay(transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation(newDir);

			yield return 0;
		}
	}
	
	public void Rotate() {
		isActive = true;

		StartCoroutine(Coroutine());
	}
#endregion
}
