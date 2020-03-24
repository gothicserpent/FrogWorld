using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

/// <summary>
/// Homes into enemies. Need to disable Projectile and Health scripts.
// TODO: bullets fire inward on themselves sometimes if there are no ememies
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
private Transform enemytarget; //target of current enemy
private Rigidbody2D rb; //the dynamic object to move
public float maxDistanceAway = 100f; // max distance of homing lock on
public float speed = 5f; //speed of missile
public float rotateSpeed = 200f; //speed of rotation
public MMFeedbacks DeathFeedbacks; //fx to spawn when object dies
public LayerMask TargetLayerMask; //layer mask for collisions - i.e. walls / enemies

void Start()
{
	rb = GetComponent<Rigidbody2D>();
	InvokeRepeating("FindClosestEnemy", 0.0f, 0.1f); //slightly more efficient
}

/*
   void Update()
   {
        FindClosestEnemy();
   }
 */

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
	//if (MinValue == 0.0f || !enemygameobjects[MinIndex].activeSelf) return;  // return if all dead or none exist
	if (enemytarget==null)
	{
		//make a fake enemy target at the mouse position
		//new Vector2(this.transform.position.x+10.0f, this.transform.position.y);
		Vector2 fakedirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - rb.position;
		fakedirection.Normalize();
		float fakerotateAmount = Vector3.Cross(fakedirection, transform.right).z;

		rb.angularVelocity = -fakerotateAmount * rotateSpeed;

		//Debug.Log("x: " + transform.right.x + ", y: " + transform.right.y + ", z: " + transform.right.z); //original transform.right

		Vector3 screenPos = GameObject.Find("FrogCamera").GetComponent<Camera>().WorldToScreenPoint(this.transform.position); //screen position calculation of this object

		//Debug.Log("screenpos this x: " + screenPos.x + ", screen this y: " + screenPos.y + ", screen this z: " + screenPos.z);
		//Debug.Log("mouse x: " + Input.mousePosition.x + ",mouse y: " + Input.mousePosition.y + ",mouse z: " + Input.mousePosition.z);

		//rb.velocity = transform.right * speed; //old calculation
		rb.velocity = Vector3.Normalize(Input.mousePosition - screenPos) * speed;
		//rb.velocity = Vector3.Dot(rb.velocity, rb.forward); //another experimental calculation
		return;
	}
	Vector2 direction = (Vector2)enemytarget.position - rb.position;
	direction.Normalize();
	float rotateAmount = Vector3.Cross(direction, transform.right).z;
	rb.angularVelocity = -rotateAmount * rotateSpeed;
	rb.velocity = transform.right * speed;
}

private void FindClosestEnemy()
{
	float distanceToClosestEnemy = Mathf.Infinity;
	GameObject closestEnemy = null;
	GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

	foreach (GameObject currentEnemy in allEnemies)
	{
		float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
		if (distanceToEnemy < distanceToClosestEnemy)
		{
			distanceToClosestEnemy = distanceToEnemy;
			closestEnemy = currentEnemy;
		}
	}

	if (closestEnemy == null) //if no enemies found
	{
		enemytarget = null;
		return;
	}
	//Debug.Log("Closest enemy distance: " + (closestEnemy.transform.position - this.transform.position).sqrMagnitude);
	if ((closestEnemy.transform.position - this.transform.position).sqrMagnitude <= maxDistanceAway) enemytarget = closestEnemy.transform; //only set the enemy target if the maxDistanceAway condition is met
	else enemytarget = null;

}

}
