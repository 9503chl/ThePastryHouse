using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    private GameSetting gameSetting;
    public GameObject BackToMenuPanel, BackToWindowPanel;
    public Slider BGMSlider;
    public Slider EffectSlider;
    public Slider GammaSlider;

    public Button BackToMenuBtn, YesBackToMenuBtn, YesBackToWindowBtn;

    private static SettingPanel instance;
    public static SettingPanel Instance { 
        get 
        { 
            if(instance == null)
            {
                SettingPanel[] templates = FindObjectsOfType<SettingPanel>();
                if(templates.Length > 0)
                {
                    instance = templates[0];
                    instance.enabled= true;
                    instance.gameObject.SetActive(true);
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        instance = this;

        gameSetting = GameSetting.Instance;

        BGMSlider.onValueChanged.AddListener(delegate { gameSetting.BGMVolunmChange(BGMSlider.value); });
        EffectSlider.onValueChanged.AddListener(delegate { gameSetting.EffectVolunmChange(EffectSlider.value); });
        GammaSlider.onValueChanged.AddListener(delegate { gameSetting.GammaChange(GammaSlider.value); });

        YesBackToMenuBtn.onClick.AddListener(delegate
        {
            gameSetting.SaveToInstance();
            PanelManager.Instance.ActiveView = PanelManager.ViewKind.Title;
            gameObject.SetActive(false);
        });

        YesBackToWindowBtn.onClick.AddListener(delegate
        {
            Application.Quit();
        });
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        Time.timeScale = 0;
        BackToMenuPanel.SetActive(false);
        BackToWindowPanel.SetActive(false);
        if (PanelManager.Instance.ActiveView == PanelManager.ViewKind.Title)
            BackToMenuBtn.interactable = false;
        else
            BackToMenuBtn.interactable = true;

        BGMSlider.value = gameSetting.options.BGMVolumn;
        EffectSlider.value = gameSetting.options.EffectVolumn;
        GammaSlider.value = gameSetting.options.Gamma;
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
