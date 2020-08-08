
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Game.Enums;

public class GameControl : ScriptableObject {

	public static GameControl control;
	public float highScore;
	public float lastScore;

	int bumperSpawnCount;
	int borderSpawnCount;

	//DEVNOTE: vol determins sprite. This is not a user set volume, but 
	// a value that is used to Adjust the volume.  Set at design time in editor.
	public float userVolAdj;

	public VolAttr activeVolume;
	
#region initialize
	public void Initialize(string ApplicationPath) 
	{
		highScore = 0.0f;
		lastScore = 0.0f;
		
		userVolAdj = 0.0f;
		bumperSpawnCount = 0;
	}
#endregion


#region SingletonPattern
    private static GameControl theInstance;
  
	public GameControl () 
	{
		if (theInstance != null)
		{
		    return;
		}
        
        theInstance = this;
	}
	
    public static GameControl Instance () 
	{
	    if (theInstance == null) 
		{
	        theInstance = ScriptableObject.CreateInstance( typeof( GameControl)) as GameControl;
			theInstance.Initialize(Application.dataPath);
	    } 
	    	
		return theInstance; 
    }

#endregion

	public static void Save() 
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data  = new PlayerData();

		data.highScore = GameControl.Instance().highScore;
		data.lastScore = GameControl.Instance().lastScore;

		data.userVolAdj = GameControl.Instance().userVolAdj;

		bf.Serialize(file, data);
		file.Close();
	}

	public static void Load()
	{
		if ( File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
	
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			Instance().lastScore = data.lastScore;
			Instance().highScore = data.highScore;
			Instance().userVolAdj = data.userVolAdj;

		}
	}

	public static void SetUserVolumneAdj( float newVolume )
	{
		Instance().userVolAdj = newVolume;
	}

	public static float GetUserVolumeAdj()
	{
		return Instance().userVolAdj;
	}

	public static void ResetHighScoreData() 
	{
		theInstance.Initialize(Application.dataPath);
		GameControl.Save();
	}

	public static int GetNextBumperId() 
	{
		return Instance().bumperSpawnCount++;
	}

	public static int GetNextBorderId() 
	{
		return Instance().borderSpawnCount++;
	}
}

public static class Extensions
{
	public static void SetTransparency(this UnityEngine.UI.Image p_image, float p_transparency)
	{
		if (p_image != null)
		{
			UnityEngine.Color __alpha = p_image.color;
			__alpha.a = p_transparency;
			p_image.color = __alpha;
		}
	}
}

[Serializable]
class PlayerData 
{
	public float highScore;
	public float lastScore;
	public float userVolAdj;
}
