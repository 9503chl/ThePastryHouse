﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : View
{
    public string TagName;

    public Transform ObstacleTarget;
    void Awake()
    {
        OnBeforeShow += GamePanel_OnBeforeShow;
    }

    private void GamePanel_OnBeforeShow()
    {
        PlayerInput.BaseInputInstance.Player = GameObject.FindGameObjectWithTag(TagName);
        if(PlayerInput.BaseInputInstance.Player != null)
        {
            PlayerInput.PlayerInputInstance.PlayerComponent = PlayerInput.PlayerInputInstance.Player.GetComponent<Player>();
        }
        WaitingManager.Instance.NeedWaiting();
        MapManager.Instance.CreateMap(ObstacleTarget);
    }
}
