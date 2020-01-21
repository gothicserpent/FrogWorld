/*
 * create objects on death
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;


public class DeathInstantiateObject : MonoBehaviour
{
Health health;
private bool instantiated = false;

[Header("Death")]
public GameObject InstantiatedOnDeath;
public float offsetX;
public float offsetY;
[Header("Chance to instantiate")]
public float chance=1.0f;


void Start()
{
	health = GetComponent<Health>();
	InvokeRepeating("CheckForDeath", 0, 0.1F);
}

// Update is called once per frame
void Update()
{
	//CheckForDeath();
}

protected virtual void CheckForDeath()
{
	if (health.CurrentHealth == 0 && InstantiatedOnDeath != null && !instantiated && health.enabled) //only test instantiated once; health.enabled needed for mech boss1
	{
		//float temp = Random.Range(0.0f, 1.0f);
		//Debug.Log("Instantiating with chance:" + temp + ">=" + chance + "set chance.");
		instantiated = true;
		if ((Random.Range(0.0f, 1.0f)) <= chance)
		{
			Instantiate(InstantiatedOnDeath, this.transform.position + new Vector3(offsetX, offsetY, 0), this.transform.rotation);
		}
	}
}

}
