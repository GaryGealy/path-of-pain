#region File Description
//-----------------------------------------------------------------------------
// UserConfig.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#endregion

public class UserConfig : MonoBehaviour {
	
#region enums
#endregion
	
#region fields
	
#endregion
	
#region properties
	public KeyCode GroundTargetCycleKey = KeyCode.T;
	public KeyCode IncAltitudeKey = KeyCode.R;
	public KeyCode DecAltitudeKey = KeyCode.E;
	public KeyCode ChangePathKey = KeyCode.P;
	
	public KeyCode CAKey_1 = KeyCode.C;
	public KeyCode CAKey_2 = KeyCode.V;
	public KeyCode CAKey_3 = KeyCode.B;
#endregion
		
#region Initialize
	// Use this for initialization
	
	public void Init() {
		// do things here that need to happen after the object is instantiated. 
		
	}
	
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() {
	
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() {
	}
#endregion
	
#region methods
	// Update is called once per frame
	void Update() {
	
	}
#endregion
	
#region GUI
#endregion

}
