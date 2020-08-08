#region File Description
//-----------------------------------------------------------------------------
// AppManager_About.cs
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

public class AppManager_About : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields

	GameControl gameControl;
	bool isActive;
	#endregion
	
	#region properties
	public GameObject activeEventManager;
	public GameObject activeAppManager;
	public GameObject activeAudioManager;

	// ShowMenu Panel Buttons
	public GameObject exitLevelBtn;
	public GameObject facebookBtn;
	public GameObject showResetBtn;
	public GameObject resetYes;
	public GameObject resetNo;

	public GameObject otherButtons;
	public GameObject allegroLogo;
	public GameObject resetHighScoresPanel;
	#endregion
	
	
	#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop += LevelStopEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -= LevelStopEvent;
	}

	void LevelStartEvent() 
	{
		isActive = true;
		//SetParticleState( makeActive: true );
	}

	void LevelStopEvent() 
	{
		isActive = false;
		//SetParticleState( makeActive: false );
	}

	void SceneLoadCompleteEvent() 
	{
		isActive = true;
		resetHighScoresPanel.SetActive( false);
	}

	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		isActive = false;
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
	
	void SetParticleState( bool makeActive ) 
	{
		//EventManager.DebugLog("SetParticleState()", ":" + makeActive.ToString());

		GameObject targetGameObject = GameObject.Find("Star Field Particles");
		if ( targetGameObject )
		{
			var particles = targetGameObject.GetComponent<ParticleSystem>();
			
			if ( makeActive )
			{	
				particles.Play();
			}
			else
			{	
				particles.Pause();
			}
		}
		else 
		{
			EventManager.DebugLog("SetParticleState()", debugMsg: "unable to find object 'Star Field Particles'" +  "reporting object: " + transform.name);
		}

	}

	public void OpenFacebookPage() 
	{	
        Application.OpenURL("https://www.facebook.com/GaryGealyAIG");
	}


    public void ShowResetButtons()
    {

		if ( showResetBtn ) showResetBtn.SetActive(false);

		ModelFlip(modelActive: true);

		activeEventManager.GetComponent<EventManager>().SignalLevelStop();
	}


	// DEVNOTE:can not pass an Enum or Unity Editor will not recognize
	public void ResetHighScores( string userAction) 
	{
		if ( userAction.ToLower() == "reset") 
		{
			GameControl.ResetHighScoreData(); 
		}

		if ( showResetBtn ) showResetBtn.SetActive(true);

		ModelFlip(modelActive: false);
		
		activeEventManager.GetComponent<EventManager>().SignalLevelStart();
	}

	void ModelFlip( bool modelActive ) 
	{
		if ( modelActive )
		{
			resetHighScoresPanel.SetActive(true);
			otherButtons.SetActive(false);
			allegroLogo.GetComponent<Image>().SetTransparency(0.5f);
		}
		else
		{
			resetHighScoresPanel.SetActive(false);
			otherButtons.SetActive(true);
			allegroLogo.GetComponent<Image>().SetTransparency(1.0f);
		}

	}
	
	public void LevelExit() 
	{
		if( isActive )
		{
			Scenes.Load("GameMain");
		}
	}
	#endregion

}
