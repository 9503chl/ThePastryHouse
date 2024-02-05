using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ScreenResolutionManager : MonoBehaviour
{
    public static ScreenResolutionManager Instance
    {
        get
        {
            ScreenResolutionManager[] templates = FindObjectsOfType<ScreenResolutionManager>();
            if (templates.Length > 0)
            {
                instance = templates[0];
                instance.enabled = true;
                instance.gameObject.SetActive(true);
            }
            return instance;
        }

    }

    private static ScreenResolutionManager instance;

    public int Width = 1920;
    public int Height = 1080;
    public bool FullScreen = true;

    public GameObject TopBorder;
    public GameObject BottonBorder;
    public GameObject LeftBorder;
    public GameObject RightBorder;

    private void Start()
    {
        ChangeResolution(Width, Height, FullScreen);
    }

    public void ChangeResolution(int width, int height, bool fullScreen)
    {
        if (width > 0 && height > 0)
        {
            Screen.SetResolution(width, height, fullScreen);
            Camera.main.GetComponent<PixelPerfectCamera>().refResolutionX= width;
            Camera.main.GetComponent<PixelPerfectCamera>().refResolutionY= height;

            TopBorder.transform.position = new Vector3(0, height / 20, 0);
            TopBorder.transform.localScale = new Vector3(width / 10, 2, 1);

            BottonBorder.transform.position = new Vector3(0, -height / 20, 0);
            BottonBorder.transform.localScale = new Vector3(width / 10, 2, 1);

            LeftBorder.transform.position = new Vector3(-width/20, 0, 0);
            LeftBorder.transform.localScale = new Vector3(2, height/10, 1);

            RightBorder.transform.position = new Vector3(width / 20, 0, 0);
            RightBorder.transform.localScale = new Vector3(2, height / 10, 1);
        }
    }
}
