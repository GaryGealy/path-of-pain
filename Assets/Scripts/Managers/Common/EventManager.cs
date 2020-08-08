#region File Description
//-----------------------------------------------------------------------------
// EventManager.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;

public class EndRoundInfoEventArgs : EventArgs
{
	public RoundResult roundResult;
	public bool spawnAnother;

	public EndRoundInfoEventArgs( RoundResult r, bool s )
	{
		roundResult = r; 
		spawnAnother = s;
	}
}

public class UpdateScoreInfoEventArgs : EventArgs
{
	public int hitCount;

	public UpdateScoreInfoEventArgs( int h )
	{
		hitCount = h; 
	}
}

public class RecordHitInfoEventArgs : EventArgs
{
	public HitType hitType;

	public RecordHitInfoEventArgs( HitType h )
	{
		hitType = h; 
	}
}

public class ChangeSpeedInfoEventArgs : EventArgs
{
	public ChangeSpeedAction changeSpeedAction;

	public ChangeSpeedInfoEventArgs( ChangeSpeedAction s)
	{
		changeSpeedAction = s; 
	}
}

public class EventManager : MonoBehaviour 
{


#region enums
#endregion
	
#region fields
#endregion
	
#region properties
	public bool debugActive = false;
#endregion

#region events
	
#endregion

#endregion

#region debugging

	public static void DebugLog(string methodName, string debugMsg )
	{
		#if UNITY_EDITOR
			Debug.Log(string.Format("<color=green>{0}</color>, {1}    {2}",Time.time, methodName, debugMsg));
		#endif
	}
#endregion


	
#region events
	// REF: https://unity3d.com/learn/tutorials/modules/intermediate/scripting/events
	// REF: https://unity3d.com/learn/tutorials/topics/scripting/delegates

	public delegate void SpawnNextWall();
	public static event SpawnNextWall OnSpawnNextWall;
	
	public void SignalSpawnNextWall() 
	{
		if (OnSpawnNextWall != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnSpawnNextWall();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}


	public delegate void ChangeSpeed( ChangeSpeedInfoEventArgs e);
	public static event ChangeSpeed OnChangeSpeed;
	
	public void SignalChangeSpeed( ChangeSpeedAction changeSpeed ) 
	{
		ChangeSpeedInfoEventArgs eventInfo = new ChangeSpeedInfoEventArgs( changeSpeed );

		if (OnChangeSpeed != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnChangeSpeed( eventInfo );
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void BeginRound();
	public static event BeginRound OnBeginRound;
	
	public void SignalBeginRound() 
	{
		if (OnBeginRound != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnBeginRound();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void EndRound(EndRoundInfoEventArgs e);
	public static event EndRound OnEndRound;
	
	public void SignalEndRound(RoundResult roundResult, bool spawnAnother ) 
	{

		EndRoundInfoEventArgs eventInfo = new EndRoundInfoEventArgs(roundResult, spawnAnother);

		if (OnEndRound != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnEndRound( eventInfo );
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void UpdateScore(UpdateScoreInfoEventArgs e);
	public static event UpdateScore OnUpdateScore;
	
	public void SignalUpdateScore( int hitCount) 
	{

		UpdateScoreInfoEventArgs eventInfo = new UpdateScoreInfoEventArgs(hitCount);
		
		if (OnUpdateScore != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnUpdateScore( eventInfo );
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void RecordHit(RecordHitInfoEventArgs e);
	public static event RecordHit OnRecordHit;
	
	public void SignalRecordHit(HitType hitType) 
	{

		RecordHitInfoEventArgs eventInfo = new RecordHitInfoEventArgs(hitType);

		if (OnRecordHit != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnRecordHit( eventInfo );
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void SceneLoadComplete();
	public static event SceneLoadComplete OnSceneLoadComplete;
	
	public void SignalSceneLoadComplete () 
	{

        if (OnSceneLoadComplete != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnSceneLoadComplete();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void LevelReady();
	public static event LevelReady OnLevelReady;
	
	public void SignalLevelReady () 
	{

		if (OnLevelReady != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnLevelReady();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void LevelStart();
	public static event LevelStart OnLevelStart; 
	
	public void SignalLevelStart () 
	{
		if (OnLevelStart != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnLevelStart();
		}
	}

	public delegate void LevelStop();
	public static event LevelStop OnLevelStop;
	
	public void SignalLevelStop () 
	{
		if (OnLevelStop != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnLevelStop();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	public delegate void InputFeedback();
	public static event InputFeedback OnInputFeedback;
	
	public void SignalInputFeedback () 
	{
		if (OnInputFeedback != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnInputFeedback();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
	}

	

	public delegate void StartTimer();
	public static event StartTimer OnStartTimer;
	
	public void SignalStartTimer() 
	{
		if (OnStartTimer != null) 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "");
			OnStartTimer();
		}
		else 
		{
			if ( debugActive ) DebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name, "<color=red>No Listeners:</color>");
		}
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
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() 
	{
	}
#endregion
	
#region methods
#endregion


}
