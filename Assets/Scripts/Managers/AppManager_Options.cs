#region File Description
//-----------------------------------------------------------------------------
// AppManager_Options.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

public class AppManager_Options : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields

	bool isActive;
	#endregion
	
	#region properties
	public GameObject activeEventManager;
	public GameObject activeAppManager;
	public GameObject activeAudioManager;
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
	void Start() 
	{

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
	
	public void LevelExit() 
	{
		if( isActive )
		{
			LoadLevel("Main");
		}
	}

	// delay load so things can complete, like song fade
	public void LoadLevel( string sceneName)
	{
 		StartCoroutine(WaitBeforeLoad(sceneName));
 	}
  
	IEnumerator WaitBeforeLoad( string sceneName )
	{
		yield return new WaitForSeconds(5);
	    SceneManager.LoadScene(sceneName);
	}
	#endregion

}
