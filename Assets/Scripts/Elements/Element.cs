#region File Description
//-----------------------------------------------------------------------------
// Element.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using Game.Enums;


namespace App.Classes
{
    public class Element
    {
        public ElementName name;
        public Color color;
    }

    public abstract class ElementHandler
    {
        //public abstract IElement FactoryMethod();
        //public abstract Color ElementColor { get; set; }
    }



}
