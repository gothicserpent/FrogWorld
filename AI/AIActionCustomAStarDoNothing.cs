/*
 * AI ACTION TO DISABLE MOVING IN A* PATHS
 */

﻿using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace MoreMountains.CorgiEngine
{

/// <summary>
/// As the name implies, an action that does nothing. Just waits there.
/// </summary>
public class AIActionCustomAStarDoNothing : AIActionAStarDoNothing
{

protected Animator m_Animator;

protected override void Initialization()
{
	base.Initialization();
	m_Animator = gameObject.GetComponent<Animator>();
}

/// <summary>
/// On PerformAction we do nothing
/// </summary>
public override void PerformAction()
{
	base.PerformAction();
	m_Animator.SetBool("detected", false);
}

}
}
