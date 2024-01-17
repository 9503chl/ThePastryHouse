using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D CursorTexture;
    void Awake()
    {
        if(CursorTexture != null)
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
