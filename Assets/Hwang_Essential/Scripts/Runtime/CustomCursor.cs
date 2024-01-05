using System;

namespace UnityEngine
{
    [Serializable]
    [CreateAssetMenu(fileName = "Custom Cursor", menuName = "Preset/Custom Cursor", order = 1001)]
    public class CustomCursor : ScriptableObject
    {
        public Texture2D Texture;
        public Vector2 HotSpot;
        public CursorMode Mode;

        private static Component ownerComponent;

        private static CustomCursor currentCursor;
        public static CustomCursor CurrentCursor
        {
            get { return currentCursor; }
        }

        public CustomCursor()
        {
        }

        public CustomCursor(Texture2D texture, Vector2 hotSpot, CursorMode mode = CursorMode.Auto)
        {
            Texture = texture;
            HotSpot = hotSpot;
            Mode = mode;
        }

        public static void Apply(Component component, CustomCursor customCursor)
        {
            if (customCursor != null)
            {
                Cursor.SetCursor(customCursor.Texture, customCursor.HotSpot, customCursor.Mode);
                currentCursor = customCursor;
                ownerComponent = component;
            }
        }

        public static void Reset(Component component)
        {
            if (ownerComponent == component)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                currentCursor = null;
                ownerComponent = null;
            }
        }
    }
}
