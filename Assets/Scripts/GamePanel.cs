﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : View
{
    public string TagName;

    public Transform ObjectTarget;
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
            PlayerInput.PlayerInputInstance.LanternComponent = PlayerInput.PlayerInputInstance.Player.GetComponentInChildren<Lantern>();
            PlayerInput.PlayerInputInstance.PlayerColliderGetComponent();
            PlayerInput.PlayerInputInstance.CharRigidbody = PlayerInput.BaseInputInstance.Player.GetComponent<Rigidbody2D>();
        }
        WaitingManager.Instance.NeedWaiting();
        MapManager.Instance.CreateMap(ObjectTarget);
        EnemyManager.Instance.EnemyCreate(ObjectTarget);
    }
}
