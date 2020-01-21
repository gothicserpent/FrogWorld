using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// the enemy character must have zero health when hitting bottom, so that the player homing bolts don't track it
/// </summary>
[AddComponentMenu("Corgi Engine/Character/Core/Zero Enemy Health Character Level Bounds")]
public class ZeroEnemyHealthCharacterLevelBounds : CharacterLevelBounds
{

protected override void ApplyBoundsBehavior(BoundsBehavior behavior, Vector2 constrainedPosition)
{
	_controller.State.TouchingLevelBounds = true;

	if ( (_character == null)
	     || (LevelManager.Instance == null) )
	{
		return;
	}

	if (behavior== BoundsBehavior.Kill)
	{
		if (_character.CharacterType == Character.CharacterTypes.Player)
		{
			LevelManager.Instance.KillPlayer (_character);
		}
		else
		{
			Health health = _character.gameObject.MMGetComponentNoAlloc<Health>();
			if (health != null)
			{
				health.Damage(health.CurrentHealth, health.gameObject, 0.0f, 0.0f);
				health.Kill();
				//public virtual void Damage(int damage,GameObject instigator, float flickerDuration, float invincibilityDuration)
			}
		}
		return;
	}

	if (behavior == BoundsBehavior.Constrain)
	{
		transform.position = constrainedPosition;
		return;
	}
}
}
}
