using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    private GameSetting gameSetting;
    public GameObject Panel;
    public Slider BGMSlider;
    public Slider EffectSlider;
    public Slider GammaSlider;

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

        gameSetting = FindObjectOfType<GameSetting>();

        BGMSlider.onValueChanged.AddListener(delegate { gameSetting.BGMVolunmChange(BGMSlider.value); });
        EffectSlider.onValueChanged.AddListener(delegate { gameSetting.EffectVolunmChange(EffectSlider.value); });
        GammaSlider.onValueChanged.AddListener(delegate { gameSetting.GammaChange(GammaSlider.value); });

        BGMSlider.value = gameSetting.options.BGMVolumn;
        EffectSlider.value = gameSetting.options.EffectVolumn;
        GammaSlider.value = gameSetting.options.Gamma;
    }
    void OnEnable()
    {
        Time.timeScale = 0;
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
