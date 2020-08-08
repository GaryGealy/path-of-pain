#region File Description
//-----------------------------------------------------------------------------
// _template.cs
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

public class TurrentHandler : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;
	GameObject activeEventManager;
	GameObject activeAppManager;

	bool holdingKeyDown = false;
	KeyCode keyPressed;

    #endregion

    #region properties
    public int AngleIncrement;
	public KeyCode KeyCodeCW;
	public KeyCode KeyCodeCounterCW;
    #endregion


    #region events
    void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop +=LevelStopEvent;
		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
		
		InputManager.OnKeyDown += KeyDownEvent;
		InputManager.OnKeyUp += KeyUpEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -=LevelStopEvent;
		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;
		
		InputManager.OnKeyDown -= KeyDownEvent;
		InputManager.OnKeyUp -= KeyUpEvent;
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

	void KeyUpEvent( KeyInfoEventArgs e)
	{
		if ( isActive)
		{
			holdingKeyDown = false;
			keyPressed = KeyCode.Space;
		}
	}
	
	void KeyDownEvent( KeyInfoEventArgs e)
    {
        if (isActive)
        {
			holdingKeyDown = true;
			keyPressed = e.keyCode;
        }
    }

    void RotateShape( KeyCode keyCode, int rotateAmt )
	{
		if ( KeyCodeCounterCW == keyCode )
		{	
			//DEVNOTE: RotateAround not accurate
			//setParent.transform.RotateAround(Vector3.zero, setRotationAround, rotateAmt);
			transform.Rotate( Vector3.forward * rotateAmt);
		}
		else if ( KeyCodeCW == keyCode )
		{
			transform.Rotate( Vector3.forward * -rotateAmt);

		}

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

		keyPressed = KeyCode.Space;
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
		if (isActive && holdingKeyDown)  
		{
 			RotateShape(keyPressed, AngleIncrement);
		}
	}
#endregion
}
