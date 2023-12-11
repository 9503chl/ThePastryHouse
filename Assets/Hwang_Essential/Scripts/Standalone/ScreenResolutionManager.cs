using System;
using UnityEngine;

public class ScreenResolutionManager : MonoBehaviour
{
    public int Width = 1920;
    public int Height = 1080;
    public bool FullScreen = true;

    private void Start()
    {
        ChangeResolution(Width, Height, FullScreen);
    }

    public void ChangeResolution(int width, int height, bool fullScreen)
    {
        if (width > 0 && height > 0)
        {
            Screen.SetResolution(width, height, fullScreen);
        }
    }
}
