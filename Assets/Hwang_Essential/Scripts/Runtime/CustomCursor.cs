using System;

namespace UnityEngine
{
    [Serializable]
    public class CustomCursor
    {
        public Texture2D Texture;
        public Vector2 HotSpot;
        public CursorMode Mode;

        public CustomCursor()
        {
        }

        public CustomCursor(Texture2D texture, Vector2 hotSpot, CursorMode mode = CursorMode.Auto)
        {
            Texture = texture;
            HotSpot = hotSpot;
            Mode = mode;
        }

        public void Apply()
        {
            if (Texture != null)
            {
                Cursor.SetCursor(Texture, HotSpot, Mode);
            }
        }

        public static void Apply(CustomCursor customCursor)
        {
            if (customCursor != null)
            {
                customCursor.Apply();
            }
        }

        public static void Reset()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
