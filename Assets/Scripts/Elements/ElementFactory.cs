#region File Description
//-----------------------------------------------------------------------------
// ElementFactory.cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//
// REF
// https://garywoodfine.com/factory-method-design-pattern/
//-----------------------------------------------------------------------------
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using Game.Enums;


namespace App.Classes
{
    public interface IElement
    {
        string elementName { get; }
    }

    public static class ElementFactory 
    {
        public static IElement Create( ElementName name )
        {
            switch (name)
            {
                case ElementName.water :
                    return new Water();

                case ElementName.earth :
                    return new Earth();

                default:
                    return new Water();
            }
        }
    }

}
