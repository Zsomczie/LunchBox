using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D crosshair;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.ForceSoftware);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
