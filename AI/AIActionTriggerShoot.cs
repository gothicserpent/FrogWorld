using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// An Action that shoots using the currently equipped weapon. If your weapon is in auto mode, will shoot until you exit the state, and will only shoot once in SemiAuto mode. You can optionnally have the character face (left/right) the target, and aim at it (if the weapon has a WeaponAim component).
/// </summary>
public class AIActionTriggerShoot : AIActionShoot
{

public float triggerPeriod = 0.5f; // amount of time to wait before re-triggering the anim
private float shootTime = 0.0f; //  gets set each time there's a shoot
protected Animator m_Animator;

/// <summary>
/// set the m_Animator var
/// </summary>
protected override void Initialization()
{
	base.Initialization();
	m_Animator = gameObject.GetComponent<Animator>();
}


/// <summary>
/// trigger the animator on performing a shoot
/// </summary>
public override void PerformAction()
{
	base.PerformAction();
	if (((Time.time - shootTime) > triggerPeriod) && (Time.time > triggerPeriod))
	{
		m_Animator.SetTrigger("Shoot");
		m_Animator.SetBool("Shooting", true);
		shootTime = Time.time;
		Debug.Log("Trigger shoot");
		Invoke("ResetShoot", triggerPeriod);
	}
}

/// <summary>
/// need to invoke this every triggerPeriod seconds because the anim needs to be reset.
/// </summary>
private void ResetShoot()
{
	m_Animator.ResetTrigger("Shoot");
	m_Animator.SetBool("Shooting", false);
}

}
}
