/*
 * create objects on death
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MoreMountains.CorgiEngine;


public class EnemyGhost : MonoBehaviour
{

public float triggerPeriod = 4.0f;  // amount of time to wait before re-triggering the anim
private float shootTime = 0.0f;  //  gets set each time there's a shoot
protected Animator m_Animator;
protected SpriteRenderer sprite;
void Start()
{
	m_Animator = gameObject.GetComponent<Animator>();
	sprite = gameObject.GetComponent<SpriteRenderer>();
	m_Animator.SetBool("idle", true);
	//InvokeRepeating("GhostProcess", 0.5f, 3.0f);
}

// Update is called once per frame
void Update()
{
	if (((Time.time - shootTime) > triggerPeriod) && (Time.time > triggerPeriod))
	{
		shootTime = Time.time;
		Debug.Log("triggered");
		m_Animator.SetBool("idle", false);
		m_Animator.SetBool("appearing", false);
		Invoke("transparent", 0.5f);
		Invoke("Idle", 1.0f);
		Invoke("Appear", (triggerPeriod/2));
	}
}

private void transparent()
{
	sprite.color = new Color (1f, 1f, 1f, 0f);
	Debug.Log("Appearing");
	m_Animator.SetBool("idle", false);
	m_Animator.SetBool("appearing", true);
	Invoke("Idle", 1.0f);
}

private void Appear()
{
	Debug.Log("Appearing");
	sprite.color = new Color (1f, 1f, 1f, 1f);
	m_Animator.SetBool("idle", false);
	m_Animator.SetBool("appearing", true);
	Invoke("Idle", 1.0f);
}

private void Idle()
{
	Debug.Log("Idle");
	m_Animator.SetBool("idle", true);
}

}
