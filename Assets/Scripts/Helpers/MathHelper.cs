#region File Description
//-----------------------------------------------------------------------------
// MathHelper.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#endregion



public static class MathHelper {
	
	#region enums
	#endregion
	
	#region fields
	#endregion
	
	#region properties
	#endregion
		
	#region Initialize
	#endregion
	
	#region methods
	
	public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
		
        return Mathf.Clamp (angle, min, max);
    }
	
	#endregion
}
