using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MoreMountains.CorgiEngine;

namespace MoreMountains.Tools
{
/// <summary>
/// Allow for custom behavior if the player is in the air vs on the ground and he's wanting to glide or run with the X button.
/// </summary>
public class MMTouchButtonRunGlide : MMTouchButton
{

protected override void Update()
{
	base.Update();
	if (GameObject.FindWithTag("Player").GetComponent<Character>().Airborne == true)
	{
		//ButtonPressedFirstTime.RemoveAllListeners();
		ButtonPressedFirstTime.AddListener(delegate {this.GetComponentInParent<InputManager>().GlideButtonDown();});
		ButtonReleased.AddListener(delegate {this.GetComponentInParent<InputManager>().GlideButtonUp();});
		ButtonPressed.AddListener(delegate {this.GetComponentInParent<InputManager>().GlideButtonPressed();});
	}
	else
	{
		//ButtonPressedFirstTime.RemoveAllListeners();
		ButtonPressedFirstTime.AddListener(delegate {this.GetComponentInParent<InputManager>().RunButtonDown();});
		ButtonReleased.AddListener(delegate {this.GetComponentInParent<InputManager>().RunButtonUp();});
		ButtonPressed.AddListener(delegate {this.GetComponentInParent<InputManager>().RunButtonPressed();});
	}
}
}
}
