
#region File Description
//-----------------------------------------------------------------------------
// FadeStance.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------

// references
// change scenes
// http://answers.unity3d.com/questions/8237/changing-scenes.html
// http://unifycommunity.com/wiki/index.php?title=Fade


#endregion

#region using
using UnityEngine;
using System.Collections;
#endregion

public class FadeStance : MonoBehaviour {

	#region fields
	private float scaleChange = 1.0f;
	private float fadeAlpha = 1.0f;
	private Color lastColor;
	private float startTime = 0.0f;
	#endregion
	
	#region properties
	public int SecToSelfDestroy = 5;
	public float FadeSpeed = 10.0f;
	public float ScaleSpeed = 0.005f;
	#endregion
		
	#region methods
	void Awake () {
	}
	
	void Start() {
		Destroy(gameObject,SecToSelfDestroy);
		lastColor = GetComponent<Renderer>().material.color;
		fadeAlpha = 1.0f;
	}
	
	void FixedUpdate() {
		
		// adjust transparency
		fadeAlpha = Mathf.Lerp(1.0f, 0.0f, ( Time.time-startTime )/ FadeSpeed);
		Color fadeColor =  new Color(lastColor.r, lastColor.g, lastColor.b, fadeAlpha);
		lastColor = fadeColor;
		
		GetComponent<Renderer>().material.color = fadeColor;
		
		// adjust size
		scaleChange = Mathf.Lerp(0.0f, 1.0f,( Time.time-startTime )/ ScaleSpeed);
		transform.localScale = new Vector3(scaleChange, scaleChange, scaleChange);
	}
	
	// Update is called once per frame
	void Update() {
	}
#endregion
	
}