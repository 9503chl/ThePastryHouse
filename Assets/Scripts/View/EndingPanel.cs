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

        FadeDuration = WaitingManager.Instance.FadeTime;

        RestartBtn.onClick.AddListener(delegate
        {
            PanelManager.Instance.ActiveView = PanelManager.ViewKind.Game;
        });

        BackToMenuBtn.onClick.AddListener(delegate
        {
            ComponentController.Instance.DisableComponents();
            PanelManager.Instance.ActiveView = PanelManager.ViewKind.Title;
        });
    }

    private void EndingPanel_OnBeforeShow()
    {
        Time.timeScale = 0;

    }
}
