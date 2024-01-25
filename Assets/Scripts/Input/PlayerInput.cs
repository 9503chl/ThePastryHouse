using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public static PlayerInput PlayerInputInstance;

    public Player PlayerComponent;
    public Lantern LanternComponent;
    public Sight2D[] sight2Ds;

    public bool isEscapeOK = true;

    public Rigidbody2D CharRigidbody;//이걸로 이동해보자.

    public Collider2D PlayerCollider2D;

    private Vector2 dir = Vector2.zero;
    
    private Camera mainCamera;

    public float CameraMaxX;
    public float CameraMaxY;

    public override void OnUpdate()
    {
        base.OnUpdate();
        #region 캐릭터 상호작용
        if (Player != null)
        {
            mainCamera.transform.position = new Vector3(Mathf.Clamp(Player.transform.position.x, -CameraMaxX, CameraMaxX),
                                                        Mathf.Clamp(Player.transform.position.y, -CameraMaxY, CameraMaxY), 
                                                        mainCamera.transform.position.z);
            if (Input.GetKeyDown(KeyCode.A)) PlayerComponent.m_Sprite.flipX = true;
            if (Input.GetKeyDown(KeyCode.D)) PlayerComponent.m_Sprite.flipX = false;
            InputAndDir();
        }
        #endregion

        #region 메뉴 상호작용
        if (Input.GetKeyDown(KeyCode.Escape) && isEscapeOK)
            SettingPanel.Instance.gameObject.SetActive(SettingPanel.Instance.gameObject.activeSelf ? false : true);
        #endregion
    }

    void InputAndDir()
    {
        dir.x = Input.GetAxis("Horizontal");  
        dir.y = Input.GetAxis("Vertical");    
        if (dir != Vector2.zero)  
        {
            transform.forward = dir;
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (Player != null)
        {
            CharRigidbody.MovePosition(CharRigidbody.position + dir * PlayerComponent.Speed * Time.deltaTime);
        }
    }
    public override void OnAwake()
    {
        base.OnAwake();                                                 
        mainCamera  = Camera.main;
        PlayerInputInstance = this;
    }

    public void PlayerColliderGetComponent()
    {
        PlayerCollider2D = Player.GetComponent<Collider2D>();
    }
    public void PlayerComponentOff()
    {
        PlayerComponent.enabled = false;
        PlayerCollider2D.enabled = false;
        LanternComponent.enabled = false;   
        for(int i = 0; i <sight2Ds.Length; i++)
            sight2Ds[i].enabled = false;
        
    }
    public void PlayerComponentOn()
    {
        PlayerComponent.enabled = true;
        PlayerCollider2D.enabled = true;
        Player.transform.position = Vector3.zero;
        LanternComponent.enabled = true;
        for (int i = 0; i < sight2Ds.Length; i++)
            sight2Ds[i].enabled = true;
    }
}