
#region File Description
//-----------------------------------------------------------------------------
// Fader.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------

// references
// https://www.youtube.com/watch?v=iV-igTT5yE4


#endregion

#region using
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;

#endregion

public class Fader : MonoBehaviour {

	#region fields

	private GameObject activeEventManager;

	#endregion
	
	#region properties
	
	public GameObject sceneFader;
	public Image faderImage;
	public Animator faderAnim;

	#endregion
		
	#region methods
	void Awake () {
	}
	
	void Start() {
		activeEventManager = GameObject.Find("EventManager");
		
		if ( !activeEventManager ) 
		{ 
			EventManager.DebugLog("Start()", "unable to find 'EventManager' reporting object: " + transform.name);
		}

        sceneFader.transform.localScale = new Vector3(Screen.width, Screen.height, 1); 
		StartCoroutine(Fading());
	}


	IEnumerator Fading() 
	{
		faderAnim.SetBool("Fade", true);
	 	yield return new WaitUntil(()=>faderImage.color.a == 1);
		
		activeEventManager.GetComponent<EventManager>().SignalSceneLoadComplete();
	}	
	#endregion
}