using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : View
{
    public GameObject LevelSelectPanel;

    public Button StartButton;

    public ButtonGroup LevelSelectGroup;

    public MissionData[] MissionDatas;

    private void Awake()
    {
        OnBeforeShow += TitlePanel_OnBeforeShow;
        StartButton.onClick.AddListener(OnStartBtnClick);

        LevelSelectGroup.onClick.AddListener(delegate { LevelSelect(LevelSelectGroup); });
    }

    private void TitlePanel_OnBeforeShow()
    {
        LevelSelectPanel.SetActive(false);
    }

    private void LevelSelect(ButtonGroup buttonGroup)
    {
        GameSetting.Instance.CurrentMissionData = MissionDatas[buttonGroup.SelectedIndex];
        PanelManager.Instance.ActiveView = PanelManager.ViewKind.Game;
    }
    private void OnStartBtnClick()
    {
        LevelSelectPanel.SetActive(true);
    }
}
    
