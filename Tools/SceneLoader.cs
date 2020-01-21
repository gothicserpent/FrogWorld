using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
public class SceneLoader : MonoBehaviour
{
// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{

}

public void Unpause()
{
	CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
}

public void ReloadScene()
{
	//LevelManager.Instance.GotoLevel(SceneManager.GetActiveScene().name);
	CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
	SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	//LoadingSceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

public void LoadScene(string level)
{
	CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
	SceneManager.LoadScene(level, LoadSceneMode.Single);
}

}
}
