#region File Description
//-----------------------------------------------------------------------------
// Singleton.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;
using System.IO;

using Game.Enums;
#endregion

public class Singleton: ScriptableObject {

	
#region enums
#endregion

#region fields
	private static int saveIndexSelectedAction;
	private static int saveIndexSelectedBoast;
#endregion

#region properties
	// used to pass time duration to levels
	public static float levelDurationStarting;
	public static float levelTimeScore;
#endregion
	
#region methods

#endregion

#region initialize
	public void Initialize(string ApplicationPath) 
	{
	}
#endregion


#region SingletonPattern
    private static Singleton theSingleInstance;
  
	public Singleton () 
	{
	
		if (theSingleInstance != null)
		{
		    return;
		}
        
        theSingleInstance = this;
	}
	
	
    public static Singleton Instance () 
	{
	    if (theSingleInstance == null) 
		{
	        theSingleInstance = ScriptableObject.CreateInstance( typeof( Singleton)) as Singleton;
			theSingleInstance.Initialize(Application.dataPath);
	    } 
	    	
		return theSingleInstance; 
    }

#endregion


#region methods
	public static string TimeFormatForDisplay( float workingValue )
	{
		int minutes = Mathf.FloorToInt(workingValue / 60F);
     	int seconds = Mathf.FloorToInt(workingValue - minutes * 60);
     	string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

     	return niceTime;
	}
    #endregion

}



#region SceneParameters
public static class Scenes {

    private static Dictionary<string, string> parameters;

    public static void Load(string sceneName, Dictionary<string, string> parameters = null) {
        Scenes.parameters = parameters;
        SceneManager.LoadScene(sceneName);
    }

    public static void Load(string sceneName, string paramKey, string paramValue) {
        Scenes.parameters = new Dictionary<string, string>();
        Scenes.parameters.Add(paramKey, paramValue);
        SceneManager.LoadScene(sceneName);
    }

    public static Dictionary<string, string> GetSceneParameters() {
        return parameters;
    }

    public static string GetParam(string paramKey) {
        if (parameters == null) return "";
        return parameters[paramKey];
    }

    public static void SetParam(string paramKey, string paramValue) {
        if (parameters == null)
            Scenes.parameters = new Dictionary<string, string>();
        Scenes.parameters.Add(paramKey, paramValue);
    }

}
#endregion


