#region File Description
//-----------------------------------------------------------------------------
// CameraExtensions.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Game.Enums;
#endregion
public static class CameraExtensions
{
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}