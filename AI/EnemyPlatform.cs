using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// Add this component to a platform and it'll be able to follow a path and carry a character
/// </summary>
public class EnemyPlatform : MovingPlatform
{

public float triggerPeriod = 2.0f; // amount of time to wait before re-triggering the anim
private float shootTime = 0.0f;    //  gets set each time there's a shoot
protected Animator m_Animator;

/// <summary>
/// Flag inits, initial movement determination, and object positioning
/// </summary>
protected override void Initialization()
{
	base.Initialization ();
	m_Animator = gameObject.GetComponentInChildren<Animator>();
}

/// <summary>
/// trigger the animator on performing a shoot (in our case , a "hit")
/// </summary>
protected override void Update()
{
	base.Update();
	//Debug.Log(CurrentSpeed.magnitude);
	//Debug.Log()
	if (((Time.time - shootTime) > triggerPeriod) && (Time.time > triggerPeriod) && (Mathf.Abs(CurrentSpeed.magnitude) < .2)) //trigger conditions are met?
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

/*
   /// <summary>
   /// Flag inits, initial movement determination, and object positioning
   /// </summary>
   protected override void Start()
   {
        base.Start();
        InvokeRepeating("TestShoot", 0.2f, 0.2f);  //tried this for a while because update wasn't working
   }
 */

/*
   private void TestShoot()
   {
        //Debug.Log("Test shoot invoked");
        if (((Time.time - shootTime) > triggerPeriod) && (Time.time > triggerPeriod) && (Mathf.Abs(CurrentSpeed.magnitude) < .2))         //if the trigger period is met
        {
                m_Animator.SetTrigger("Shoot");
                m_Animator.SetBool("Shooting", true);
                shootTime = Time.time;
                Debug.Log("Trigger shoot");
                Invoke("ResetShoot", triggerPeriod);
        }
   }
 */


/*
   //neither of these triggered, so i'll have to use a velocity check with a timer.
   public override void ResetEndReached()
   {
        base.ResetEndReached();
        Debug.Log("End reached");
        //_endReached = false;
   }

   public override void ChangeDirection()
   {
        base.ChangeDirection();
        Debug.Log("Direction changed");
   }
 */
}
}
