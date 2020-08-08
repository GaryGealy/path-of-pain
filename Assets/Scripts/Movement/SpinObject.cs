#region File Description
//-----------------------------------------------------------------------------
// SpinObject.cs
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

public class SpinObject : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields

	bool isActive;

	#endregion
	
	#region properties

	public float rotationSpeed;
	public Vector3 rotatation;

	#endregion
	
	
	#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;
	}

	void SceneLoadCompleteEvent() 
	{
		isActive = true;
	}
	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() {
	
	}
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() {
		isActive = false;
	}
	#endregion
	
	#region methods
	// Update is called once per frame
	void Update() {	

		if ( isActive )
		{
			transform.Rotate(rotatation, rotationSpeed * Time.deltaTime );
		}
	}
	#endregion
	
}
