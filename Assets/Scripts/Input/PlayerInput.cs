using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public static PlayerInput PlayerInputInstance;

    public Player PlayerComponent;

    public bool isEscapeOK = true;

    public Rigidbody2D CharRigidbody;//이걸로 이동해보자.

    public Collider2D collider2D;

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
        dir.x = Input.GetAxis("Horizontal");   // x축 방향 키 입력
        dir.y = Input.GetAxis("Vertical");     // z축 방향 키 입력
        if (dir != Vector2.zero)   // 키입력이 존재하는 경우
        {
            transform.forward = dir;	// 키 입력 시, 입력된 방향으로 캐릭터의 방향을 바꿈
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (Player != null)//아직 화면 벗어나기 설정안함
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
        collider2D = Player.GetComponent<Collider2D>();
    }
    public void PlayerColliderOff()
    {
        collider2D.enabled = false;
    }
    public void PlayerColliderOn()
    {
        collider2D.enabled = true;
    }
}