#region File Description
//-----------------------------------------------------------------------------
// ShakeIt.cs
//
// Copyright (C) Allegro Interactive Games. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

public class ShakeIt : MonoBehaviour 
{
	
#region enums
#endregion
	
#region fields
	bool isActive;

	float duration;
	bool noStop;
	bool itsShaking;
	float pauseDuration;

	Vector3 startPosition;
#endregion
	
#region properties
	public float shakeSpeed;
	public bool	shakeOnStart;

	// use a value > 0 to make transform shake in that direction.
	public Vector3 shakeDirection;
	public Vector3 shakeDistance;
	
#endregion
	
	
#region events
	void OnEnable() 
	{
		// make event subscriptions
		EventManager.OnLevelStart += LevelStartEvent;
		EventManager.OnLevelStop +=LevelStopEvent;

        EventManager.OnEndRound += EndRoundEvent;
        EventManager.OnBeginRound += BeginRoundEvent;
    }

	void OnDisable()
	{
		// remove event subscriptions
		EventManager.OnLevelStart -= LevelStartEvent;
		EventManager.OnLevelStop -=LevelStopEvent;

        EventManager.OnEndRound -= EndRoundEvent;
        EventManager.OnBeginRound -= BeginRoundEvent;
    }


	void LevelStartEvent() 
	{
		isActive = true;
	}

	void LevelStopEvent() 
	{
		isActive = false;
	}

    void BeginRoundEvent()
    {
    }

    void EndRoundEvent(EndRoundInfoEventArgs e)
    {
        StartShaking();
    }
    #endregion

    #region Initialize
    //The Start function is called after all Awake functions on all script instances have been called. 
    void Start() 
	{
		if ( shakeOnStart ) 
		{
			StartShaking();
		}
    }
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() 
	{
		itsShaking = false;
	}
#endregion
	
#region methods
	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator ShakeCoroutine() 
	{
		while(itsShaking && isActive)
		{

			duration -= Time.deltaTime;

            // DEVNOTE: z won't matter if transform is 2D
            transform.position = startPosition + 
  						new Vector3( Mathf.Sin(Time.time * shakeSpeed) * shakeDirection.x * shakeDistance.x, 
  									Mathf.Sin(Time.time * shakeSpeed)  * shakeDirection.y * shakeDirection.y, 
  									Mathf.Sin(Time.time * shakeSpeed) * shakeDirection.z * shakeDistance.z);
			
  			// DEVNOTE: this need to be after shake so that reposition 
  			// to start position work correctly.
  			if ( !noStop )
  			{
				if ( duration <= 0.0f) 
				{
					itsShaking = false;
                    transform.position = startPosition;
				}
			}

			yield return null;
		}

	}

	public void Shake( float secDuration, float secPauseDuration, bool shouldShakeStop ) 
	{
		duration = secDuration;
		noStop = shouldShakeStop;

		startPosition = transform.position;

		itsShaking = true;
		isActive = true;

		StartCoroutine(ShakeCoroutine());
	}

	public void StartShaking() 
	{
		Shake(10, 5.0f, true);
	}
#endregion
}
