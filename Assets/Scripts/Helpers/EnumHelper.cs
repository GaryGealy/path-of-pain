#region File Description
//-----------------------------------------------------------------------------
// cs
//
// Copyright (C) Allegro Interactive. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

#endregion

namespace Game.Enums 
{
	
	public enum Swipe { None, Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft  };

	public enum ElementName { water, air, fire, earth, lightning, frost, magma, clay};

	public enum EnemyType { cylinder, triangle, cube, pentagon, hex };

	public enum AudioVolume
	{
		off = 0,
		low,
		medium,
		high
	}
	
	public enum ClickType 
	{
		MouseUp = 0,
		MouseDown = 1
	}

	public enum ColorSchemeLabel {
		White = 0,
		Blue,
		DarkBlue,
		Green,
		Red,
		Yellow
	}


	public enum AudioToPlay { 
		WaveComplete = 1,
		WaveFailed, 
		GameOver, 
		Ready, 
		Set, 
		Go
	}

	public enum RoundResult {
		Fail = 1, 
		Pass
	}

	public enum MoveType { 
		up = 1, 
		down, 
		left,
		right 
	}

	public enum HitType {
		backwall = 1,
		playerwall
	}

	public enum ShapeSpinDirection {
		left = 1,
		right
	}

	public enum ShapeStyle {
		bar = 1,
		center_corner,
		tee,
        corners,
        zee, 
        six, 
        odd_two, 
        hook,
        why,
        odd_one,
        odd_three,
        odd_four
    }

	public enum StreakType {
		miss = 1,
		match
	}
	
	public enum StreakLevel {
		none = 1,
		low, 
		medium,
		high
	}

	public enum ChangeSpeedAction {
		faster = 1,
		slower
	}
}