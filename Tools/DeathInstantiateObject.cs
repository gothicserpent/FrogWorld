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
protected bool instantiated = false;

[Header("Death")]
public GameObject InstantiatedOnDeath;
public float offsetX;
public float offsetY;


void Start()
{
	health = GetComponent<Health>();
}

// Update is called once per frame
void Update()
{
	CheckForDeath();
}

protected virtual void CheckForDeath()
{
	if (health.CurrentHealth == 0 && InstantiatedOnDeath != null && !instantiated)
	{
		instantiated = true;
		Instantiate(InstantiatedOnDeath, this.transform.position + new Vector3(offsetX, offsetY, 0), this.transform.rotation);
	}
}

}
