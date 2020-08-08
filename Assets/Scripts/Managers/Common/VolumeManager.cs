#region File Description
//-----------------------------------------------------------------------------
// VolumeManager.cs
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

[System.Serializable]
public class VolAttr 
{
	public Sprite speakerSprite;
	public float volume;
}

public class VolumeManager : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;
	GameObject activeAudioManager;

#endregion
	
#region properties
	public GameObject volumeButton;
	public List<VolAttr> attributeSet;
	Queue attributeFiFo;
	//public VolAttr activeVolume;
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
	}
	
#endregion
		
#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		activeEventManager = GameObject.Find("EventManager");
		if ( !activeEventManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'EventManager' reporting object: " + transform.name);
		}
		
		activeAppManager = GameObject.Find("AppManager");
		if ( !activeAppManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'AppManager' reporting object: " + transform.name);
		}

		activeAudioManager = GameObject.Find("AudioManager");
		if ( !activeAudioManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'AudioManager' reporting object: " + transform.name);
		}

		attributeFiFo = new Queue();
		foreach( VolAttr i in attributeSet )
		{
			attributeFiFo.Enqueue( i );
			GameControl.Instance().activeVolume = i;
		}
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

	void UpdateSpriteIcon( VolAttr volAttr ) 
	{
		Image spriteCompoment;
		spriteCompoment = volumeButton.GetComponentInChildren<Image>();

		if ( spriteCompoment )
		{
			spriteCompoment.sprite = volAttr.speakerSprite;
		}	
		else
		{
			EventManager.DebugLog("AdjustVolume()", "unable to find spriteComponent, reporting object: " + transform.name);
		}
	}

	public void SetVolume( float newVol ) 
	{
		VolAttr dequeVolAttr;
		
		int queueCount = attributeFiFo.Count;
		int checkCount = 0;

		while( checkCount <= queueCount )
		{
			dequeVolAttr = (VolAttr) attributeFiFo.Dequeue();

			// send to the back of the line.
			UpdateSpriteIcon( dequeVolAttr );
			attributeFiFo.Enqueue(dequeVolAttr);

			if ( dequeVolAttr.volume == newVol ) 
			{
				GameControl.Instance().activeVolume = dequeVolAttr;
				break;
			}

			checkCount++;
		}

		//In case a match is not found, which should not happen :-)
		if ( checkCount > queueCount )
		{
			GameControl.Instance().activeVolume = (VolAttr) attributeFiFo.Dequeue();
			UpdateSpriteIcon( GameControl.Instance().activeVolume );
			attributeFiFo.Enqueue(GameControl.Instance().activeVolume);
			
			EventManager.DebugLog("SetVolume()", "unable to find volumn name in queue, reporting object: " + transform.name);
		}
	}


	public void AdjustVolume()
	{
		if ( isActive) 
		{
			//DEVNOTE: queue will be endless, take from Front, add back in to back
			GameControl.Instance().activeVolume = (VolAttr) attributeFiFo.Dequeue();
			attributeFiFo.Enqueue(GameControl.Instance().activeVolume);

			if ( volumeButton )
			{
				activeAudioManager.GetComponent<AudioManager>().MakeUserVolumeAdj(GameControl.Instance().activeVolume.volume);
				UpdateSpriteIcon(GameControl.Instance().activeVolume);
				GameControl.SetUserVolumneAdj(GameControl.Instance().activeVolume.volume);
				GameControl.Save();
			}
			else
			{
				EventManager.DebugLog("AdjustVolume()", "unable to find volumeButton, reporting object: " + transform.name);
			}
		}
	}
#endregion
}
