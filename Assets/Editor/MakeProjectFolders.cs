// generate standard project folders

using UnityEditor;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class MakeProjectFolders : MonoBehaviour {

	
	[MenuItem ("Allegro Tools/Generate Folders")]
	static void GenerateFolders() {
		
		GenerateFoldersWork();
	}
	
	
	static void GenerateFoldersWork() {
		
		string ActiveDir = Application.dataPath;
		
		// setup listof folders to create
		List<string> Folders = new List<string>();
		Folders.Add("Audio");
		Folders.Add("Materials");
		Folders.Add("Meshes");
		Folders.Add("Fonts");
		Folders.Add("Textures");
		Folders.Add("Resources");
		Folders.Add("Scripts");
		Folders.Add("Shaders");
		Folders.Add("Packages");
		Folders.Add("Physics");
		Folders.Add("Scenes");
		Folders.Add("Prefabs");
		
		foreach( string Folder in Folders ) {
			string newPath = System.IO.Path.Combine(ActiveDir,Folder);
			
			//create new folder
			System.IO.Directory.CreateDirectory(newPath);
			
			Debug.Log("Allegro Tools/Generate Folders->Created: " +  newPath);
		}
			
		
		
	}
	
}
