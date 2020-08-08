#region File Description
//-----------------------------------------------------------------------------
// TextManager.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
// 
// DEVNOTE:
// This will be the place to put in dynamic Text lists as in those 
// created by other users. 
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

public class TextManager : MonoBehaviour 
{
	
	#region enums
	#endregion
	
	#region fields
	
	private GameObject activeEventManager;
	private GameObject activeAppManager;

	#endregion
	
	#region properties

	#endregion
	
	
	#region events
	void SetupSubscriptions()
	{
		
	}
	
	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		SetupSubscriptions();
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
		
	}
	#endregion
	
	#region GUI
	#endregion
	
}
