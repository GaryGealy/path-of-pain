
#region File Description
//-----------------------------------------------------------------------------
// AppManager_Opening.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


using System;

using App.Classes;
using Game.Enums;
#endregion

public class AppManager : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields
	bool isActive;

	#endregion
	
	#region properties
 	public GameObject activeEventManager;
 	public GameObject activeAudioManager;
	public GameObject activeScoreManager;
	public GameObject activeVolumeManager;
	public GameObject activeEnemyManager;

	public List<ColorEntry> ElementList;

	#endregion
	
	#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop +=LevelStopEvent;

		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
		 SceneManager.sceneLoaded += OnSceneLoaded;

		EventManager.OnLevelStart += LevelStartEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -=LevelStopEvent;

		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent; 
		SceneManager.sceneLoaded -= OnSceneLoaded;

		EventManager.OnLevelStart -= LevelStartEvent;
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
		GameControl.Save();
	}

	  // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    }
	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		isActive = false;
		GameControl.Load();

		activeAudioManager.GetComponent<AudioManager>().MakeUserVolumeAdj(GameControl.GetUserVolumeAdj());
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


	//DEVNOTE: called from OnClick() event
	public void StartGame() 
	{	 
		activeEventManager.GetComponent<EventManager>().SignalLevelStart();
		
	}

	//DEVNOTE: called from OnClick() event
	public void QuitGame() 
	{
		Application.Quit();
	}


	public void OpenAboutScene() 
	{
		if ( isActive )
		{
			Scenes.Load("About");
        }
	}
	#endregion

	// Color Management
	public ColorEntry GetElementColor( ElementName elementName )
	{
		ColorEntry result = ElementList.Find(x => x.name == elementName); 
		return result;
	}

	public ColorEntry GetElementColor( int index )
	{
		ColorEntry result = ElementList[index];
		return result;
	}


}
