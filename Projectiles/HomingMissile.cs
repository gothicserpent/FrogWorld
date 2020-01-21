using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using System.Linq;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

/// <summary>
/// Homes into enemies. Need to disable Projectile and Health scripts.
// TODO: Do not track dead enemies.
// TODO: Sometimes, the bullets don't track properly
// TODO: Range is not working properly.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
private Transform enemytarget; //target of current enemy
private Rigidbody2D rb; //the dynamic object to move
private float MinValue=0.0f; //distance of closest enemy
private int MinIndex=0; //index array of closest enemy
private GameObject[] enemygameobjects; //array of enemy game objects
public float maxDistanceAway = 30f; // max distance of homing lock on
public float speed = 5f; //speed of missile
public float rotateSpeed = 200f; //speed of rotation
public MMFeedbacks DeathFeedbacks; //fx to spawn when object dies
public LayerMask TargetLayerMask; //layer mask for collisions - i.e. walls / enemies

void Start()
{
	rb = GetComponent<Rigidbody2D>();
	FindClosestEnemy();
}

void OnTriggerEnter2D(Collider2D collider)
{
	if (!this.isActiveAndEnabled)
	{
		return;
	}

	// if what we're colliding with isn't part of the target layers, we do nothing and exit
	if (!MMLayers.LayerInLayerMask(collider.gameObject.layer,TargetLayerMask))
	{
		return;
	}

	//Debug.Log("This object: " + this.gameObject.name + " collided with: "+ collider.gameObject.name + "in layer: " + collider.gameObject.layer);
	DeathFeedbacks?.PlayFeedbacks();
//Destroy(gameObject);
	gameObject.SetActive(false); //trying this to prevent destroy access errors - seems to work!

}

void FixedUpdate()
{
	if (MinValue == 0.0f || !enemygameobjects[MinIndex].activeSelf) return;  // return if all dead or none exist
	Vector2 direction = (Vector2)enemytarget.position - rb.position;
	direction.Normalize();
	float rotateAmount = Vector3.Cross(direction, transform.right).z;
	rb.angularVelocity = -rotateAmount * rotateSpeed;
	rb.velocity = transform.right * speed;
}

/// <summary>
/// Find the closest enemy to target
/// </summary>
void FindClosestEnemy()
{
	enemygameobjects = GameObject.FindGameObjectsWithTag("Enemy");

	Debug.Log("Enemy game objects:" + enemygameobjects.Length);

	if (enemygameobjects.Length == 0) return; // return if there are no enemies

	//Debug.Log(enemygameobjects.Length + " enemies.");

	float[] enemydistances = new float[enemygameobjects.Length];

	for (int i = 0; i < enemygameobjects.Length; i++)         // find the closest target
	{
		if (Debug.isDebugBuild) Debug.Log("Enemy " + i + " Health: " + enemygameobjects[i].GetComponent<Health>().CurrentHealth);
		if (enemygameobjects[i].GetComponent<Health>() == null)
		{
			//Debug.Log("Null health.");
			enemydistances[i]=0.0f;
		}
		if(enemygameobjects[i].GetComponent<Health>().CurrentHealth <= 0)
		{
			enemydistances[i]=0.0f;         // set enemy distance to be ludicrously high if dead
		}
		else
		{
			float dist = Vector3.Distance(this.transform.position, enemygameobjects[i].transform.position);
			if(dist <= maxDistanceAway) enemydistances[i]=dist; // set enemy distance for each index
			else enemydistances[i]=0.0f;
		}
	}

	MinValue = enemydistances.Min();
	MinIndex = enemydistances.ToList().IndexOf(MinValue);

	if (MinValue == 0.0f || !enemygameobjects[MinIndex].activeSelf) return; // return if all dead or none exist


	Debug.Log("Closest enemy transform:" + enemygameobjects[MinIndex].transform.position + " Distance away: " + MinValue);

	enemytarget = enemygameobjects[MinIndex].transform;
}
}
