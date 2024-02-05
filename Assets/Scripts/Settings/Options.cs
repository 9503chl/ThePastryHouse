using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Options : ScriptableObject
{
    public float EffectVolumn = 1f;
    public float BGMVolumn = 1f;
    public float Gamma;

    public void LoadFromJson(JsonData jsonData)//딕셔너리다 기억하자.
    {
        if (jsonData.ContainsKey("EffectVolumn"))
            EffectVolumn = float.Parse(jsonData["EffectVolumn"].ToString());
        
        if (jsonData.ContainsKey("BGMVolumn"))
            BGMVolumn = float.Parse(jsonData["BGMVolumn"].ToString());
        if (jsonData.ContainsKey("Gamma"))
            Gamma = float.Parse(jsonData["Gamma"].ToString());
    }
}
