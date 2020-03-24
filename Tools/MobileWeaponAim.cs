
//CLASS SHOULD BE ADDED ABOVE WEAPONAIM2D COMPONENT IN PLAYABLE CHARACTERS WEAPON PREFABS SO THAT IT FIRES PROPERLY ON MOBILE
//SHOULD KEEP AN EYE ON Assets/CorgiEngine/Common/Scripts/Agents/Weapons/WeaponAim.cs line 243 GetCurrentAim() to see if it changes
﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.UI;

namespace MoreMountains.CorgiEngine
{
[AddComponentMenu("Corgi Engine/Weapons/Mobile Weapon Aim")]
public class MobileWeaponAim : WeaponAim
{
/// <summary>
/// Computes the current aim direction
/// </summary>
protected override void GetCurrentAim()
{
	if (_weapon.Owner == null)
	{
		return;
	}

	if ((_weapon.Owner.LinkedInputManager == null) && (_weapon.Owner.CharacterType == Character.CharacterTypes.Player))
	{
		return;
	}

	switch (AimControl)
	{
	case AimControls.Off:
		if (_weapon.Owner == null) { return; }

		_currentAim = Vector2.right;
		_direction = Vector2.right;
		if (_characterGravity != null)
		{
			_currentAim = _characterGravity.transform.right;
			_direction = _characterGravity.transform.right;
		}
		break;

	case AimControls.Script:
		_currentAim = (_weapon.Owner.IsFacingRight) ? _currentAim : -_currentAim;
		_direction = -(transform.position - _currentAim);
		break;

	case AimControls.PrimaryMovement:
		if ((_weapon.Owner == null) || (_weapon.Owner.LinkedInputManager == null))
		{
			return;
		}

		if (_weapon.Owner.IsFacingRight)
		{
			_currentAim = _weapon.Owner.LinkedInputManager.PrimaryMovement;
			_direction = transform.position + _currentAim;
		} else
		{
			_currentAim = -_weapon.Owner.LinkedInputManager.PrimaryMovement;
			_direction = -(transform.position - _currentAim);
		}

		if (_characterGravity != null)
		{
			_currentAim = MMMaths.RotateVector2 (_currentAim,_characterGravity.GravityAngle);
			if (_characterGravity.ShouldReverseInput())
			{
				_currentAim = -_currentAim;
			}
		}
		break;

	case AimControls.SecondaryMovement:
		if ((_weapon.Owner == null) || (_weapon.Owner.LinkedInputManager == null))
		{
			return;
		}

		if (_weapon.Owner.IsFacingRight)
		{
			_currentAim = _weapon.Owner.LinkedInputManager.SecondaryMovement;
			_direction = transform.position + _currentAim;
		}
		else
		{
			_currentAim = -_weapon.Owner.LinkedInputManager.SecondaryMovement;
			_direction = -(transform.position - _currentAim);
		}
		break;

	case AimControls.Mouse:
		if (_weapon.Owner == null)
		{
			return;
		}

		#if UNITY_ANDROID || UNITY_IPHONE
		int Arrows = 300; // bottom left arrow area (previous 170)
		int XYBATopY = 300; // bottom right XYBA area Y (previous 180)
		int XYBALeftX = (int)Screen.width - 300; // bottom right XYBA area X (previous 180)
		int PauseY = (int)Screen.height - 200; // top left pause area y (previous 125)
		int PauseX = 200; // top left pause area x (previous 125)
		int InvX = (int)Screen.width - 200; // Top right inventory area X (previous 150)
		for(int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).position.x>Arrows && Input.GetTouch(i).position.x<XYBALeftX && Input.GetTouch(i).position.y<XYBATopY)
			{ _mousePosition = Input.GetTouch(i).position; }
			else if (Input.GetTouch(i).position.y>=XYBATopY && Input.GetTouch(i).position.y<=PauseY)
			{ _mousePosition = Input.GetTouch(i).position; }
			else if (Input.GetTouch(i).position.y>PauseY && Input.GetTouch(i).position.x>PauseX && Input.GetTouch(i).position.x<InvX)
			{ _mousePosition = Input.GetTouch(i).position; }
		}
		#else
		_mousePosition = Input.mousePosition;
		#endif
		_mousePosition.z = 10;

		_direction = _mainCamera.ScreenToWorldPoint (_mousePosition);
		_direction.z = transform.position.z;

		if (_weapon.Owner.IsFacingRight)
		{
			_currentAim = _direction - transform.position;
		}
		else
		{
			_currentAim = transform.position - _direction;
		}
		break;
	}
}
}
}
