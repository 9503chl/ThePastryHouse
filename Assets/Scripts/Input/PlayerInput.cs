﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public static PlayerInput PlayerInputInstance;

    public Player PlayerComponent;

    public override void OnUpdate()
    {
        base.OnUpdate();
        #region 캐릭터 상호작용
        if (Player != null)//아직 화면 벗어나기 설정안함
        {
            if (Input.GetKey(KeyCode.W)) Player.transform.Translate(0, PlayerComponent.Speed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.S)) Player.transform.Translate(0, -PlayerComponent.Speed * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.A)) Player.transform.Translate(-PlayerComponent.Speed * Time.deltaTime, 0, 0);
            if (Input.GetKey(KeyCode.D)) Player.transform.Translate(PlayerComponent.Speed * Time.deltaTime, 0, 0);
        }
        #endregion

        #region 메뉴 상호작용
        if (Input.GetKeyDown(KeyCode.Escape))
            SettingPanel.Instance.gameObject.SetActive(SettingPanel.Instance.gameObject.activeSelf ? false : true);
        #endregion
    }
    public override void OnAwake()
    {
        base.OnAwake();
        PlayerInputInstance = this;
    }
}