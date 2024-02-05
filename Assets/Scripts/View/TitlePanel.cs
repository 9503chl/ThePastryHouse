using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : View
{
    public GameObject LevelSelectPanel;

    public Button NewGameBtn;
    public Button ContinueBtn;
    public Button PanelExitBtn;

    public Text LastPlayTimeTxt;

    public ButtonGroup LevelSelectGroup;

    public MissionData[] MissionDatas;

    private SaveData saveData;

    private void Awake()
    {
        OnBeforeShow += TitlePanel_OnBeforeShow;

        NewGameBtn.onClick.AddListener(NewGameInvoke);
        ContinueBtn.onClick.AddListener(ContinueInvoke);
        PanelExitBtn.onClick.AddListener(delegate
        {
            LevelSelectPanel.SetActive(false);
            PlayerInput.PlayerInputInstance.isEscapeOK = true;
        });

        LevelSelectGroup.onClick.AddListener(delegate { LevelSelect(LevelSelectGroup); });

        saveData = GameSetting.Instance.CurrentSaveData;

        FadeDuration = WaitingManager.Instance.FadeTime;
    }

    private void TitlePanel_OnBeforeShow()
    {
        HPManager.Instance.InfoGroup.SetActive(false);
        LevelSelectPanel.SetActive(false);
        if(saveData.IsFirst)
        {
            ContinueBtn.interactable = false;
            LastPlayTimeTxt.text = string.Format("(없음)");
        }
        else
        {
            ContinueBtn.interactable = true;
            LastPlayTimeTxt.text = string.Format("({0})", saveData.LastPlayTime.ToString("yyyy:MM:dd:HH:mm:ss"));
        }
    }

    private void LevelSelect(ButtonGroup buttonGroup)
    {
        GameSetting.Instance.ListNPoolReset();

        GameSetting.Instance.CurrentMissionData = MissionDatas[buttonGroup.SelectedIndex];

        saveData.DataReset();
        saveData.CurrentLevel = 1;
        saveData.Difficulty = (MissionLevel)buttonGroup.SelectedIndex;

        PanelManager.Instance.ActiveView = PanelManager.ViewKind.Game;
    }
    private void NewGameInvoke()
    {
        LevelSelectPanel.SetActive(true);

        PlayerInput.PlayerInputInstance.isEscapeOK = false;
    }
    private void ContinueInvoke()
    {
        GameSetting.Instance.CurrentMissionData = MissionDatas[(int)saveData.Difficulty];
        GameSetting.Instance.ListNPoolReset();

        switch (saveData.CurrentLevel)
        {
            case 1:
                PanelManager.Instance.ActiveView = PanelManager.ViewKind.Game;
                break;
        }
    }
}
