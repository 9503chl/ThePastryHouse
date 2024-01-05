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
        EffectVolumn = float.Parse(jsonData["EffectVolumn"].ToString());
        BGMVolumn = float.Parse(jsonData["BGMVolumn"].ToString());
        Gamma = float.Parse(jsonData["Gamma"].ToString());
    }
}
