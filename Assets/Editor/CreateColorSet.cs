// generate color set 
using UnityEditor;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class MakeColorSet : MonoBehaviour {
	
	
	[MenuItem ("Allegro Tools/Create Color Set ")]
	static void CreateColorSet() {
		CreateColorSetWork();
	}
	
	
	static void CreateColorSetWork() {
		
		Material newMaterial;
		
		string colorName;
		
		for (int i = 0; i<=4; i++ ) {
			newMaterial = new Material(Shader.Find("Diffuse"));
			colorName = "color_" + i.ToString() + ".mat";
			
			AssetDatabase.CreateAsset(newMaterial, "Assets/Resources/ColorSets/" + colorName);
        	
			// Print the path of the created asset
        	Debug.Log(AssetDatabase.GetAssetPath(newMaterial));
        	
		}
	}
}
