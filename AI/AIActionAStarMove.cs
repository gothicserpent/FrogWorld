/*
 * AI ACTION TO ENABLE MOVING IN A* PATHS
 */
//test

﻿using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static namespace Pathfinding.AIPath;

using Pathfinding;

namespace MoreMountains.CorgiEngine
{

/// <summary>
/// As the name implies, an action that does nothing. Just waits there.
/// </summary>
public class AIActionAStarMove : AIAction
{

IAstarAI ai;

/// <summary>
/// On init we grab our CharacterMovement ability
/// </summary>
public override void Initialization()
{
	ai = GetComponent<IAstarAI>();
}

/// <summary>
/// On PerformAction we do nothing
/// </summary>
public override void PerformAction()
{
	if(!ai.canMove)
	{
		ai.canMove = true;
	}
}
}
}
