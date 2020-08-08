
#region File Description
//-----------------------------------------------------------------------------
// SizePulse.cs
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

public class SizePulse : MonoBehaviour {
	
	#region enums
	#endregion
	
	#region fields
	
	private GameObject activeEventManager;
	private GameObject activeTextManager;

	private Vector3 centerScreen;

	// the increment amount for Spin Speed
	// to get from Start to End spin speed in SpinDuration
	private float changeInterval;

	private bool thisActive;
	#endregion
	
	#region properties
	public Vector3 startScale;
	public Vector3 toScale;
	#endregion
	
	
	#region events
	void OnEnable() 
	{

	}

	void OnDisable() 
	{

	}
	#endregion
	
	#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{

		activeTextManager = GameObject.Find("TextManager");
		if ( activeTextManager ) 
		{ 	
		} 
		else 
		{ 
			Debug.LogError("unable to find 'TextManager' object", transform);
		}

/*
		// use the Editor to set the initial location, well need to reset 
		// to this after each change in text.
		startScale = transform.localScale;

		thisActive = true;
		GetComponent<Text>().enabled = true;
		GetComponent<Text>().text = activeTextManager.GetComponent<TextManager>().GetNextAction();
		StartCoroutine(PulseCoroutine());
		*/
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
	
	IEnumerator PulseCoroutine() 
	{


		while ( thisActive )
		 {

/*
			float pulse = Mathf.PingPong(Time.time, 0.5f);
			transform.localScale = startScale +  new Vector3( pulse, pulse, pulse );
			Debug.Log(transform.localScale.ToString());
			*/

			/*
			if (Vector3.Distance(transform.localScale, toScale) > 0.01f) 
			{
				transform.localScale =  Vector3.Lerp(transform.localScale, toScale, Time.deltaTime);
			} 
			else 
			{
				transform.localScale = toScale;
				thisActive = false;
			}
			*/

			yield return 0;
		}


	}
	
	public void StartPulse() 
	{
		thisActive = true;
		transform.localScale = startScale;	
		
		GetComponent<Text>().text = "Pulse Test";
		StartCoroutine(PulseCoroutine());
	}

	public void SuspendPulse() 
	{
		thisActive = false;
	}

	public void StopPulse()
	{
		thisActive = false;
	}
	#endregion
}

