#region File Description
//-----------------------------------------------------------------------------
// SpinnerControl.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion


public class SpinnerControl : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields
	private bool isActive  = false;
	private float rachetTimer;
	private float feedbackTimer;
	#endregion
	
	#region properties
	public float rotationSpeed;
	public Vector3 rotatation;
	public float rachetStep;
	public float rachetInterval;
	public float speedMax;

	public GameObject activeEventManager;

	public bool activeOnStartEvent = false;
	public bool activeOnReadyEvent = false;
	public bool activeOnLoadCompleteEvent = false;
	public bool activeOnStartTimeerEvent = false;
	
	public bool useFeedbackSound;
	public AudioSource feedbackSound;

	public float feedbackRachetStep;
	public float feedbackInterval;
	public float feedbackPitchMax;
	#endregion
	
	
	#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop +=LevelStopEvent;
		EventManager.OnLevelReady += LevelReadyEvent;

		EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -=LevelStopEvent;
		EventManager.OnLevelReady -= LevelReadyEvent;

		EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;
	}

	void LevelStartEvent() 
	{
		if ( activeOnStartEvent ) {
			isActive = true;

			if ( useFeedbackSound) {
				feedbackSound.Play();
			}
		} 
	}

	void LevelStopEvent() 
	{
		isActive = false;

			if ( useFeedbackSound ) {
				feedbackSound.Pause();
			}
	}
	
	public void LevelReadyEvent() 
	{
		if ( activeOnReadyEvent ) {
			isActive = true;

			if ( useFeedbackSound) {
				feedbackSound.Play();
			}
		}
	}

	public void SceneLoadCompleteEvent() 
	{
		if ( activeOnLoadCompleteEvent ) {
			isActive = true;

			if ( useFeedbackSound) {
				feedbackSound.Play();
			}
		}
	}
	
	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() {
	}
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() {
		rachetTimer = 0.0f;
		feedbackTimer = 0.0f;
	}
	#endregion
	
	#region methods
	// Update is called once per frame
	void Update() {	

		if ( isActive )
		{
			transform.Rotate(rotatation, rotationSpeed * Time.deltaTime);

		}
	}

	void FixedUpdate() 
	{
		if ( isActive )
		{
			if ( rachetTimer++ >= rachetInterval )
			{
				if ( rotationSpeed <= speedMax ) {
					rotationSpeed += rachetStep;
				}
				rachetTimer = 0.0f;
			}

			if ( feedbackTimer++ >= feedbackInterval)
			{
				if ( feedbackSound.pitch <= feedbackPitchMax) {
					feedbackSound.pitch += feedbackRachetStep;
				}
				feedbackTimer = 0.0f;
			}
		}

	}
	#endregion
}
