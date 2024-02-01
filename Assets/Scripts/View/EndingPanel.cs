
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanel : View
{
    public Button RestartBtn;
    public Button BackToMenuBtn;
    private void Awake()
    {
        OnBeforeShow += EndingPanel_OnBeforeShow;
        OnAfterShow += EndingPanel_OnAfterShow;

        FadeDuration = WaitingManager.Instance.FadeTime;

        RestartBtn.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            PanelManager.Instance.ActiveView = PanelManager.ViewKind.Game;
        });

        BackToMenuBtn.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            ComponentController.Instance.DisableComponents();
            PanelManager.Instance.ActiveView = PanelManager.ViewKind.Title;
        });
    }

    private void EndingPanel_OnAfterShow()
    {
        Time.timeScale = 0;
    }

    private void EndingPanel_OnBeforeShow()
    {


    }
}
