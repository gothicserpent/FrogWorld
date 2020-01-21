using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MoreMountains.CorgiEngine
{
/// <summary>
/// A Retro adventure dedicated class that will load the next level
/// </summary>
public class CustomFinishLevel : FinishLevel
{
/// <summary>
/// Loads the next level
/// </summary>
public override void GoToNextLevel()
{
	//MMEventManager.TriggerEvent(new MMGameEvent("Save")); //save current progress to disk
	SceneManager.LoadScene(LevelName, LoadSceneMode.Single); // load the new level
	//MMEventManager.TriggerEvent(new MMGameEvent("Load")); //load the progress from disk
}
}
}
