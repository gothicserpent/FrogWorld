using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// A simple component meant to be added to the pause button
/// </summary>
public class CustomPauseButton : MonoBehaviour
{
/// Puts the game on pause
public virtual void PauseButtonAction()
{
	// we trigger a Pause event for the GameManager and other classes that could be listening to it too
	ToggleCharacter(); // if this isn't done, the character does not move after the pause event
	if(GameManager.Instance.Paused) CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
	else CorgiEngineEvent.Trigger(CorgiEngineEventTypes.Pause);
}

private void ToggleCharacter()
{
	foreach (Character player in LevelManager.Instance.Players)
	{

		CharacterPause characterPause = player.GetComponent<CharacterPause>();
		if (characterPause == null)
		{
			break;
		}

		if (GameManager.Instance.Paused)
		{
			characterPause.UnPauseCharacter();
		}
		else
		{
			characterPause.PauseCharacter();
		}
	}
}
}
}
