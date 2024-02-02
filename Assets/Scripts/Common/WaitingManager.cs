using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WaitingManager : MonoBehaviour
{
    public static WaitingManager Instance;

    public GameObject WaitingBG;

    public Image ProgressiveImage;

    public float FadeTime;

    public Text PercentText;
    public Text MainText;

    private float currentTime;

    public int dotSpeed;

    public string FirstMainText;
    public string SecondMainText;
    public string ThirdMainText;

    private string[] dotTexts = new string[4] { "" , "." , ".." , "..."};

    private void Awake()
    {
        Instance = this;
        WaitingBG.SetActive(false);
    }
    public void WaitngForStart()
    {
        ProgressiveImage.fillAmount = 0;
        currentTime = 0;
        StartCoroutine(StartWaiting());
    }
    IEnumerator StartWaiting()
    {
        ComponentController.Instance.DisableComponents(); 

        WaitingBG.SetActive(true);
        int whileCnt = 0;
        int dotCnt = 0;

        while (currentTime < 3.5f)
        {
            currentTime += Time.unscaledDeltaTime;
            if (whileCnt == 0) dotCnt++;
            if(currentTime < 1)
                MainText.text = string.Format("{0}{1}", FirstMainText, dotTexts[dotCnt % 4]);
            else if(currentTime < 2) 
                MainText.text = string.Format("{0}{1}", SecondMainText, dotTexts[dotCnt % 4]);
            else
                MainText.text = string.Format("{0}{1}", ThirdMainText, dotTexts[dotCnt % 4]);
            whileCnt++;
            whileCnt %= dotSpeed;
            ProgressiveImage.fillAmount = currentTime / 3.5f;
            PercentText.text = string.Format("{0}%", (currentTime * 100 / 3.5f).ToString("F0"));
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);

        }
        ProgressiveImage.fillAmount = 1;
        WaitingBG.SetActive(false);
        currentTime = 0;

        ComponentController.Instance.EnableComponents();

        if (GameSetting.Instance.CurrentSaveData.IsFirst)
        {
            GameSetting.Instance.SaveGameData();
            GameSetting.Instance.SaveToInstance();
            GameSetting.Instance.CurrentSaveData.IsFirst = false;
        }
        AstarPath.active.Scan();
    }
}
