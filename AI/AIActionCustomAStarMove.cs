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

public class AIActionCustomAStarMove : AIActionAStarMove
{

protected Animator m_Animator;

public override void Initialization()
{
	base.Initialization();
	m_Animator = gameObject.GetComponent<Animator>();
}

public override void PerformAction()
{
	base.PerformAction();
	m_Animator.SetBool("detected", true);
}

}
}
