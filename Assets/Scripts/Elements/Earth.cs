
#region File Description
//-----------------------------------------------------------------------------
// Earth.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

using App.Classes;
using Game.Enums;

public class Earth : IElement
{
    private string _name;
    public string elementName
    {
        get => _name;
    }

    public Earth()
    {
        _name = "Earth";
    }
}