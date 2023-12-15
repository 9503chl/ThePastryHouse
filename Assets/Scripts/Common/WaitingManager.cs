using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingManager : MonoBehaviour
{
    public static WaitingManager Instance;

    public GameObject WaitingBG;

    public Image ProgressiveImage;

    public Text PercentText;
    public Text MainText;

    private float currentTime;

    private void Awake()
    {
        Instance = this;
        WaitingBG.SetActive(false);
    }
    public void NeedWaiting()
    {
        ProgressiveImage.fillAmount = 0;
        StartCoroutine(WaitingCor());
    }
    IEnumerator WaitingCor()
    {
        while (currentTime > 0)
        {

        }
        yield return null;
    }
}
