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
    public void NeedWaiting()
    {
        ProgressiveImage.fillAmount = 0;
        currentTime = 0;
        Time.timeScale = 0f;//이거 왜 하는거지.
        StartCoroutine(WaitingCor());
    }
    IEnumerator WaitingCor()
    {
        WaitingBG.SetActive(true);
        PlayerInput.PlayerInputInstance.isEscapeOK = false;
        PlayerInput.PlayerInputInstance.ColliderOff();
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
        PlayerInput.PlayerInputInstance.isEscapeOK = true;
        Time.timeScale = 1;
        PlayerInput.PlayerInputInstance.ColliderOn();
        EnemyManager.Instance.EnemyComponentOn();
        AstarPath.active.Scan();
    }
}
