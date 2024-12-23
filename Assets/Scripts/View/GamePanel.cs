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
        OnAfterShow += GamePanel_OnAfterShow;

        FadeDuration = WaitingManager.Instance.FadeTime;
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

        ObjectTarget.gameObject.SetActive(false);
    }

    private void GamePanel_OnAfterShow()
    {
        ObjectTarget.gameObject.SetActive(true);
        WaitingManager.Instance.WaitngForStart();

        MapManager.Instance.CreateProps(ObjectTarget);
        EnemyManager.Instance.CreateProps(ObjectTarget);
        SnackManager.Instance.CreateProps(ObjectTarget);
    }
}
