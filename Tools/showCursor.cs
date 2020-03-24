using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showCursor : MonoBehaviour
{
// Start is called before the first frame update

//public Texture2D cursorTexture;
//public CursorMode cursorMode = CursorMode.Auto;
//public Vector2 hotSpot = Vector2.zero;

void Start()
{
	if (!Cursor.visible) Cursor.visible = true;
}

// Update is called once per frame
void Update()
{
	if (!Cursor.visible) Cursor.visible = true;
}

}
