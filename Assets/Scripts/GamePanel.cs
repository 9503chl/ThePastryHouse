using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : View
{
    public string TagName;
    void Awake()
    {
        OnBeforeShow += GamePanel_OnBeforeShow;
    }

    private void GamePanel_OnBeforeShow()
    {
        PlayerInput.Instance.Player = GameObject.FindGameObjectWithTag(TagName);
    }
}
