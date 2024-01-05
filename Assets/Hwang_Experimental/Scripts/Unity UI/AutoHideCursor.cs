using System;
using UnityEngine;

namespace UnityEngine.UI
{
    [DisallowMultipleComponent]
    public class AutoHideCursor : MonoBehaviour
    {
        public float HideCursorTime = 5f;

        [NonSerialized]
        private bool isHideCursor = false;

        [NonSerialized]
        private float inputlessTime = 0f;

        [NonSerialized]
        private Vector2 lastMousePosition = Vector2.zero;

        private void Update()
        {
            if (Input.anyKey || DidMouseMoveOrWhellScroll())
            {
                if (inputlessTime > HideCursorTime)
                {
                    inputlessTime = 0f;
                    if (isHideCursor)
                    {
                        isHideCursor = false;
                        Cursor.visible = true;
                    }
                }
            }
            else
            {
                if (!isHideCursor)
                {
                    inputlessTime += Time.unscaledDeltaTime;
                    if (inputlessTime > HideCursorTime)
                    {
                        isHideCursor = true;
                        Cursor.visible = false;
                    }
                }
            }
        }

        private bool DidMouseMoveOrWhellScroll()
        {
            Vector2 mouseMovement = (Vector2)Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;
            if (Mathf.Abs(mouseMovement.x) >= 2f || Mathf.Abs(mouseMovement.y) >= 2f)
            {
                return true;
            }
            return Input.mouseScrollDelta != Vector2.zero;
        }
    }
}
