#region File Description
//-----------------------------------------------------------------------------
// InputManager.cs
//
// Copyright (C) Allegro Interactive Games. All rights reserved.
//
// Force push
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

#region event_classes
public class MouseInfoEventArgs : EventArgs
{
	public int mouseButton;
	public Vector3 position;

	public MouseInfoEventArgs( Vector3 p, int b )
	{
		mouseButton = b; 
		position = p;
	}
}

public class KeyInfoEventArgs : EventArgs
{
	public KeyCode keyCode;

	public KeyInfoEventArgs( KeyCode k)
	{
		keyCode = k; 
	}
}

#endregion

public class InputManager : MonoBehaviour 
{
	public delegate void MouseUp( MouseInfoEventArgs e);
	public static event MouseUp OnMouseUp;
	
	public void SignalMouseUp ( Vector3 position, int mouseButton ) 
	{
		MouseInfoEventArgs eventInfo = new MouseInfoEventArgs( position, mouseButton );
		if (OnMouseUp != null) 
		{
			OnMouseUp(eventInfo);
		}
	}

	public delegate void MouseDown( MouseInfoEventArgs e);
	public static event MouseDown OnMouseDown;
	
	public void SignalMouseDown ( Vector3 position, int mouseButton ) 
	{
		MouseInfoEventArgs eventInfo = new MouseInfoEventArgs( position, mouseButton );
		if (OnMouseDown != null) 
		{
			OnMouseDown(eventInfo);
		}
	}

	public delegate void MouseActive( MouseInfoEventArgs e);
	public static event MouseActive OnMouseActive;

	public void SignalMouseActive ( Vector3 position, int mouseButton ) 
	{
		MouseInfoEventArgs eventInfo = new MouseInfoEventArgs( position, mouseButton );
		if (OnMouseActive != null) 
		{
			OnMouseActive(eventInfo);
		}
	}

	public delegate void KeyDown( KeyInfoEventArgs e);
	public static event KeyDown OnKeyDown;
	
	public void SignalKeyDown ( KeyCode keyCode ) 
	{
		KeyInfoEventArgs eventInfo = new KeyInfoEventArgs( keyCode );
		if (OnKeyDown != null) 
		{
			OnKeyDown(eventInfo);
		}
	}

	public delegate void KeyUp( KeyInfoEventArgs e);
	public static event KeyUp OnKeyUp;
	
	public void SignalKeyUp ( KeyCode keyCode ) 
	{
		KeyInfoEventArgs eventInfo = new KeyInfoEventArgs( keyCode );
		if (OnKeyUp != null) 
		{
			OnKeyUp(eventInfo);
		}
	}

#region enums
#endregion
	
#region fields
	bool isActive = false;
#endregion
	
#region properties
public GameObject activePosManager;
#endregion


#region events
	void OnEnable() 
	{
		// make event subscriptions
        EventManager.OnBeginRound += OnBeginRoundEvent;
	}

	void OnDisable()
	{
		// remove event subscriptions
        EventManager.OnBeginRound -= OnBeginRoundEvent;
    }

    void OnBeginRoundEvent()
    {
        isActive = true;
    }
    
	
#endregion

#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
		isActive = true;
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
        if ( isActive )
        {
            if (Input.multiTouchEnabled)
            {
                KeyAndMouseInput();
                HandleSwipeMouse();
            }
            else
            {
                TouchInput();
            }
        }
		
	}
#endregion

#region swiping

	public float maxTime;
	public float minSwipeDist;

	float startTime;
	float endTime;

	Vector3 startPos;
	Vector3 endPos;
	float swipeDistance;
	float swipeTime;

	void HandleSwipeTouch() 
	{

		if(Input.touchCount > 0) 
		{

	        Touch touch = Input.GetTouch(0);
	        if (touch.phase == TouchPhase.Began)
	        {
	            startTime = Time.time;
	            startPos = touch.position;
	        }
	        else if (touch.phase == TouchPhase.Ended)
	        {
	            endTime = Time.time;
	            endPos = touch.position;

	            swipeDistance = (endPos - startPos).magnitude;
	            swipeTime = endTime - startTime;

	            if(swipeTime < maxTime && swipeDistance>minSwipeDist)
	            {
	                SwipeFunc();
	            }
	        }
	    } 
	}

	void HandleSwipeMouse() 
	{

		if (Input.GetMouseButtonDown(0))
		{
			//EventManager.DebugLog("GetMouseButtonDown", "");

			startTime = Time.time;
			startPos = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			//EventManager.DebugLog("GetMouseButtonUp", "");

			endTime = Time.time;
			endPos = Input.mousePosition;

			swipeDistance = (endPos - startPos).magnitude;
			swipeTime = endTime - startTime;

			if(swipeTime < maxTime && swipeDistance>minSwipeDist)
			{
				//EventManager.DebugLog("SwipeFunc", "");
				SwipeFunc();
			}
		}
	}

	void SwipeFunc()
	{
		//DEVNOTE:
		// swipe is being used for a rotational movement.
		// swipe down on left of screen with rotate object counter clockwise
		// but swipe on right side of screen should rotate object clockwise
		
	    Vector2 distance = endPos - startPos;

	    // check for vertical or horizontal swipes
	    if(Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
	    {
	       // Debug.Log("Horizontal swipe");
		    if( endPos.x < startPos.x)
		    {
	       		//Debug.Log("Horizontal swipe left");
	       		if ( startPos.y > Screen.height / 2 )
	       		{
					//stub
				}
				else
				{
					//stub
				}
		    }
		    else
		    {
				//Debug.Log("Horizontal swipe right");

				if ( startPos.y > Screen.height / 2 )
	       		{
					//stub
				}
				else
				{
					//stub
				}
		    }
	    }
	    else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
	    {
			//DEVNOTE: need to check for left or right of center because
			// a linear motion is controlling a rotating object. for example,
			// a down swipe on left of screen rotates object counter clockwise, 
			// but a down swipe down on right side of screen should rotate
			// object clockwise

	        //Debug.Log(" vertical  swipe");\
	        if( endPos.y < startPos.y)
		    {
	       		//Debug.Log("Horizontal swipe down");
	       		if ( startPos.x < Screen.width / 2 )
	       		{
					//stub
				}
				else
				{
					//stub;
				}
		    }
		    else
		    {
				//Debug.Log("Horizontal swipe up");
				if ( startPos.x < Screen.width / 2 )
	       		{
					//stub
				}
				else
				{
					//stub
				}
		    }
	    }



	}
#endregion

#region KeyInput
	void HandleKeyDown( KeyCode keyCode )
	{
		SignalKeyDown( keyCode );
	}

	void HandleKeyUp( KeyCode keyCode )
	{
		SignalKeyUp( keyCode );
	}
#endregion

#region MouseInput
	void HandleMouseDown( Vector3 position, int button) 
	{
		SignalMouseDown( position, button );
	}

	void HandleMouseActive( Vector3 position, int button) 
	{
		SignalMouseActive( position, button );
	}
	
	void HandleMouseUp( Vector3 position, int button) 
	{
		SignalMouseUp( position, button );
	}

	#endregion
	
#region methods
	

	void TouchInput() {
		int nbTouches = Input.touchCount;
		
		if(nbTouches > 0)
		{
			for (int i = 0; i < nbTouches; i++)
			{
				Touch touch = Input.GetTouch(i);
				
				TouchPhase phase = touch.phase;
				
				switch(phase)
				{
				case TouchPhase.Began:
					HandleMouseDown(touch.position, 0);
					//print("New touch detected at position " + touch.position + " , index " + touch.fingerId);
					break;
				case TouchPhase.Moved:
					//print("Touch index " + touch.fingerId + " has moved by " + touch.deltaPosition);
					break;
				case TouchPhase.Stationary:
					HandleMouseActive(touch.position, 0);
					//print("Touch index " + touch.fingerId + " is stationary at position " + touch.position);
					break;
				case TouchPhase.Ended:
					HandleMouseUp(touch.position, 0);
					//print("Touch index " + touch.fingerId + " ended at position " + touch.position);
					break;
				case TouchPhase.Canceled:
					//print("Touch index " + touch.fingerId + " cancelled");
					break;
				}
			}
		}
	}
	
	void KeyAndMouseInput() 
	{ 
		if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0) 
		{
			//left
			HandleMouseDown( Input.mousePosition, 0 );
		} 
		else if ( Input.GetMouseButton(0) && GUIUtility.hotControl == 0 ) 
		{
			//left
			HandleMouseActive(Input.mousePosition, 0 );
		} 
		else if ( Input.GetMouseButtonUp(0) && GUIUtility.hotControl == 0 ) 
		{
			//left
			HandleMouseUp(Input.mousePosition, 0 );
		}  
		else if ( Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0 ) 
		{
			//Right
			HandleMouseDown(Input.mousePosition, 1 );
		} 
		else if ( Input.GetMouseButton(1) && GUIUtility.hotControl == 0 ) 
		{
			//Right
			HandleMouseActive(Input.mousePosition, 1 );
		} 
		else if ( Input.GetMouseButtonUp(1) && GUIUtility.hotControl == 0 ) 
		{
			//Right
			HandleMouseUp(Input.mousePosition, 1 );
		}    
		else if ( Input.GetMouseButtonDown(2) && GUIUtility.hotControl == 0 ) 
		{
			//Middle
			HandleMouseDown(Input.mousePosition, 2 );
		} 
		else if ( Input.GetMouseButton(2) && GUIUtility.hotControl == 0 ) 
		{
			//Middle
			HandleMouseActive(Input.mousePosition, 2 );
		} 
		else if ( Input.GetMouseButtonUp(2) && GUIUtility.hotControl == 0 ) 
		{
			//Middle
			HandleMouseUp(Input.mousePosition, 2 );
		} else {

			foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))){
	             
	             if(Input.GetKeyDown(vKey)){
	             	HandleKeyDown( vKey );
	             	break;
	             } 
	         }

			 foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))){
	             
	             if(Input.GetKeyUp(vKey)){
	             	HandleKeyUp( vKey );
	             	break;
	             } 
	         }
	     }
	}
#endregion
}