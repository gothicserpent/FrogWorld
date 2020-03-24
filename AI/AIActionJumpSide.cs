using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// This action performs the defined number of jumps. Look below for a breakdown of how this class works.
/// </summary>
public class AIActionJumpSide : AIActionJump
{

public float triggerPeriod = 1.0f; // amount of time to wait before triggering first jump anim
protected CorgiController _controller;
protected Animator m_Animator;

public override void Initialization()
{
	base.Initialization();
	_controller = this.gameObject.GetComponent<CorgiController>();
	m_Animator = gameObject.GetComponentInChildren<Animator>();
}

public override void PerformAction()
{
	base.PerformAction();

	// apply horizontal force
	float randomnumber = Random.Range(-1.0f, 1.0f);

	_controller.SetHorizontalForce(randomnumber*Mathf.Sqrt( 2f * _characterJump.JumpHeight * Mathf.Abs(_controller.Parameters.Gravity) ));
}

// Update is called once per frame
void Update()
{
	//Debug.Log(_controller.Speed.y);
	if(Time.time>triggerPeriod)
	{
		if (_controller.Speed.y > 1)
		{
			m_Animator.SetFloat("charspeed", 10.0f);
			m_Animator.SetBool("jumping", true);
		}
		else if (_controller.Speed.y < -1)
		{
			m_Animator.SetFloat("charspeed", -10.0f);
			m_Animator.SetBool("jumping", true);
		}
		else
		{
			m_Animator.SetFloat("charspeed", 0.0f);
			m_Animator.SetBool("jumping", false);
		}
	}
}

}
}
