using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.Feedbacks;

namespace MoreMountains.Tools
{
/// <summary>
/// When bullets die in midair, this can be used to spawn an effect.
/// </summary>
public class PoolableObjectDestroyEffect : MMPoolableObject
{
public MMFeedbacks DeathFeedbacks;
/// <summary>
/// Turns the instance inactive, in order to eventually reuse it.
/// </summary>
public override void Destroy()
{
	base.Destroy();
	DeathFeedbacks?.PlayFeedbacks();
}

}
}
