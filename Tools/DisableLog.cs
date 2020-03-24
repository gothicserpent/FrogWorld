using UnityEngine;

/// <summary>
/// Disable logs if needed.
/// </summary>
public class DisableLog : MonoBehaviour
{

public bool disableLogs = false;

// Start is called before the first frame update
void Start()
{
	if (disableLogs) Debug.unityLogger.logEnabled = false;
	//else Debug.logger.logEnabled = true;
}

// Update is called once per frame
void Update()
{

}

}
