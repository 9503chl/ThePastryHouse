using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public static PlayerInput PlayerInputInstance;

    public Player PlayerComponent;

    public float BaseSpeed;

    public bool isEscapeOK = true;

    public Rigidbody CharRigidbody;//이걸로 이동해보자.

    private Camera mainCamera;
    public override void OnUpdate()
    {
        base.OnUpdate();
        #region 캐릭터 상호작용
        if (Player != null)//아직 화면 벗어나기 설정안함
        {
            mainCamera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, mainCamera.transform.position.z);

            if (Input.GetKey(KeyCode.W)) CharRigidbody.AddForce(new Vector3(0, PlayerComponent.Speed * Time.deltaTime * BaseSpeed, 0), ForceMode.VelocityChange);
            if (Input.GetKey(KeyCode.S)) CharRigidbody.AddForce(new Vector3(0, -PlayerComponent.Speed * Time.deltaTime * BaseSpeed, 0), ForceMode.VelocityChange);
            if (Input.GetKey(KeyCode.A)) CharRigidbody.AddForce(new Vector3(-PlayerComponent.Speed * Time.deltaTime * BaseSpeed, 0, 0), ForceMode.VelocityChange);
            if (Input.GetKey(KeyCode.D)) CharRigidbody.AddForce(new Vector3(PlayerComponent.Speed * Time.deltaTime * BaseSpeed, 0, 0), ForceMode.VelocityChange);
        }
        #endregion

        #region 메뉴 상호작용
        if (Input.GetKeyDown(KeyCode.Escape) && isEscapeOK)
            SettingPanel.Instance.gameObject.SetActive(SettingPanel.Instance.gameObject.activeSelf ? false : true);
        #endregion
    }
    
    public override void OnAwake()
    {
        base.OnAwake();
        mainCamera  = Camera.main;  
        PlayerInputInstance = this;
    }
}