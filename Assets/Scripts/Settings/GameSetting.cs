using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    public MissionData CurrentMissionData;

    public Options options;
    
    public GameObject BGMAudioGroup;
    public GameObject EffectAudioGroup;
    public SpriteRenderer GammaPanel;

    private AudioSource[] BGMAudios;
    private AudioSource[] EffectAudios;

    void Awake()
    {
        Instance = this;

        BGMAudios = BGMAudioGroup.GetComponentsInChildren<AudioSource>();
        EffectAudios = EffectAudioGroup.GetComponentsInChildren<AudioSource>();

        foreach (AudioSource a in BGMAudios)
        {
            a.volume = options.BGMVolumn;
        }
        foreach (AudioSource a in EffectAudios)
        {
            a.volume = options.EffectVolumn;
        }
        GammaPanel.color = new Color(GammaPanel.color.r, GammaPanel.color.g, GammaPanel.color.b, options.Gamma);
    }
    public void BGMVolunmChange(float value)
    {
        foreach (AudioSource a in BGMAudios)
        {
            a.volume = value;
        }
        options.BGMVolumn = value;
    }
    public void EffectVolunmChange(float value)
    {
        foreach (AudioSource a in EffectAudios)
        {
            a.volume = value;
        }
        options.EffectVolumn = value;
    }
    public void GammaChange(float value)
    {
        GammaPanel.color = new Color(GammaPanel.color.r, GammaPanel.color.g, GammaPanel.color.b, 
            0.5f - options.Gamma/2);
        options.Gamma = value;
    }
}
