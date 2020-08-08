    // See http://answers.unity3d.com/questions/9844/copypaste-guistyle-in-the-inspector.html
    //
    @CustomEditor(GUISkin)
    class GUISkinExtensions extends Editor  {
		static final var builtinGUISkinStyles : String[] = [
			"box", "button", "toggle",
			"label", "textField", "textArea",
			"window",
			"horizontalSlider", "horizontalSliderThumb",
			"verticalSlider", "verticalSliderThumb",
			"horizontalScrollbar", "horizontalScrollbarThumb",
			"horizontalScrollbarLeftButton", "horizontalScrollbarRightButton",
			"verticalScrollbar", "verticalScrollbarThumb",
			"verticalScrollbarUpButton", "verticalScrollbarDownButton",
			"scrollView"
	    ];
     
	    var from = 0;
	    var to = 0;
	     
	    function OnInspectorGUI() {
		    var skin = target as GUISkin;
		     
		    DrawDefaultInspector();
		     
		    EditorGUILayout.Space();
		     
		    var names = builtinGUISkinStyles;
		    var customNames = new String[skin.customStyles.Length];
		    for (var i=0; i<customNames.Length; ++i) {
		    	customNames[i] = skin.customStyles[i].name;
		    }
		    
		    names += customNames;
		     
		    from = EditorGUILayout.Popup("From:",from,names);
		    to = EditorGUILayout.Popup("To:",to,names);
		    if (GUILayout.Button("Copy")) {
		    var fs = skin.GetStyle(names[from]);
			    var ts = skin.GetStyle(names[to]);
			    var newStyle = new GUIStyle(fs);
			    newStyle.name = ts.name;
			    var custom = to - builtinGUISkinStyles.Length;
			    
			    if (custom >= 0) {
			    	skin.customStyles[custom] = newStyle;
			    } else {
			    	skin.GetType().InvokeMember(names[to], System.Reflection.BindingFlags.SetProperty, null, skin, [newStyle]);
			    }
		    }
	    }
    }