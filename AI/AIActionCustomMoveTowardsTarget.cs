using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// Special move towards target, to change the default behavior when moving towards the target.
/// </summary>
public class AIActionCustomMoveTowardsTarget : AIActionMoveTowardsTarget
{

protected Animator m_Animator;
public float MovementMagnitude = 1.0f;

public override void Initialization()
{
	base.Initialization();
	m_Animator = gameObject.GetComponent<Animator>();
}


/// <summary>
/// need to set the flag to true here
/// </summary>
public override void PerformAction()
{
	base.PerformAction();
	m_Animator.SetBool("detected", true);
}

/// <summary>
/// set the flag to false for these
/// </summary>
public override void OnEnterState()
{
	base.OnEnterState();
	m_Animator.SetBool("detected", false);
}

public override void OnExitState()
{
	base.OnEnterState();
	m_Animator.SetBool("detected", false);
}


protected override void Move()
{
	base.Move();

	if (this.transform.position.x < _brain.Target.position.x)
	{
		_characterHorizontalMovement.SetHorizontalMove(MovementMagnitude);
	}
	else
	{
		_characterHorizontalMovement.SetHorizontalMove(-Mathf.Abs(MovementMagnitude));
	}
}

}
}
