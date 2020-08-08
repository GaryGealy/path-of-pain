#region File Description
//-----------------------------------------------------------------------------
// AutoDestructParticleSystem.cs
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

[RequireComponent (typeof(ParticleSystem))]
public class AutoDestructParticleSystem : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields
	private ParticleSystem activeParticleSystem;
	#endregion
	
	#region properties
	#endregion
	
	
	#region events
	void SetupSubscriptions() {
		
	}
	
	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() {
		SetupSubscriptions();

		activeParticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() {
	}
	#endregion
	
	#region methods
	// Update is called once per frame
	void Update() {
		if ( !activeParticleSystem.IsAlive()) {
			Destroy(gameObject);
		}
	}
	#endregion
	
	#region GUI
	#endregion
	
}
