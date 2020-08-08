#region File Description
//-----------------------------------------------------------------------------
// AudioManager.cs
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

public class AudioManager : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	private AudioSource audioWaveComplete;
	private  GameObject activeEventManager;
#endregion
	
#region properties
	public float playClipVolume;

	public float cvNewHighScore;
    public float cvStartLevel;
	public float cvEnemyHit;

	public AudioClip sndNewHighScore;
    public AudioClip sndStartLevel;
	public AudioClip sndEnemyHit;
#endregion
	
#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelReady += LevelReadyEvent;

		EventManager.OnLevelStop += LevelStopEvent;
		EventManager.OnLevelStart += LevelStartEvent;

		EventManager.OnEndRound += EndRoundEvent;
		EventManager.OnBeginRound += BeginRoundEvent;
		
		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelReady -= LevelReadyEvent;

		EventManager.OnLevelStop -= LevelStopEvent;
		EventManager.OnLevelStart -= LevelStartEvent;

		EventManager.OnEndRound -= EndRoundEvent;
		EventManager.OnBeginRound -= BeginRoundEvent;
		
		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;
	}

	void SceneLoadCompleteEvent() 
	{
		StartLevel();
	}

	void LevelReadyEvent() 
	{
	}

	void LevelStopEvent() 
	{	
	}

	void LevelStartEvent() 
	{
	}

	void EndRoundEvent(EndRoundInfoEventArgs e)
	{
    }
	
	void BeginRoundEvent()
	{	
	}
	
#endregion	
		
#region Initialize
	// Use this for initialization
	
	public void Init() 
	{
		// do things here that need to happen after the object is instantiated. 
	}
	
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		activeEventManager = GameObject.Find("EventManager");
		if ( !activeEventManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'EventManager' reporting object: " + transform.name);
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.volume = 1; // GameControl.GetUserVolumeAdj();
		audioSource.Play();
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


#region PlayClip
	public void PlayClip( AudioClip theClip )
	{
		this.PlayClip(theClip, playClipVolume);
	}

	public void PlayClip( AudioClip theClip, float volume) 
	{	
		//AudioSource.PlayClipAtPoint(theClip, Camera.main.transform.position, volume * GameControl.Instance().activeVolume.volume);	
		
		AudioSource.PlayClipAtPoint(theClip, Camera.main.transform.position, 5);	
		//EventManager.DebugLog("PlayClip()", theClip.name + ":" + (volume * GameControl.Instance().activeVolume.volume).ToString() + ":" + transform.name);
	}

#endregion

#region EventSpecificSounds
	
	public void NewHighScore() 
	{
		PlayClip(sndNewHighScore, cvNewHighScore);
	}

    public void StartLevel()
    {
        PlayClip(sndStartLevel, cvStartLevel);
    }

	public void MakeUserVolumeAdj( float newVolume)
	{
		AudioSource audioSource = GetComponent<AudioSource>();
		//audioSource.volume = newVolume;
		GameControl.SetUserVolumneAdj( newVolume );
	}

	public void EnemyHit()
	{
		PlayClip( sndEnemyHit, cvEnemyHit);
	}
    
#endregion

}
