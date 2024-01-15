using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController : MonoBehaviour
{
    public static ComponentController Instance;

    private MonoBehaviour[] monoBehaviours;

    private List<MonoBehaviour> CanDiableMonos = new List<MonoBehaviour>();
    private void Awake()
    {
        Instance = this;

        monoBehaviours = GetComponentsInChildren<MonoBehaviour>();
        for(int i = 0; i < monoBehaviours.Length; i++)
        {
            switch(monoBehaviours[i])
            {
                case EnemyManager: CanDiableMonos.Add(monoBehaviours[i]); break;
                case PlayerInput: CanDiableMonos.Add(monoBehaviours[i]); break;
                case HPManager: CanDiableMonos.Add(monoBehaviours[i]); break;
            }
        }
    }
    public void DisableComponents()
    {
        for(int i = 0; i < CanDiableMonos.Count; i++)
        {
            switch (monoBehaviours[i])
            {
                case EnemyManager:
                    EnemyManager.Instance.EnemyComponentOff();
                    break;
                case PlayerInput:
                    PlayerInput.PlayerInputInstance.isEscapeOK = false;
                    PlayerInput.PlayerInputInstance.PlayerComponentOff();
                    break;
                case HPManager: 
                    HPManager.Instance.enabled = false; 
                    break;
            }
        }
    }
    public void EnableComponents()
    {
        for (int i = 0; i < CanDiableMonos.Count; i++)
        {
            switch (monoBehaviours[i])
            {
                case EnemyManager:
                    EnemyManager.Instance.EnemyComponentOn();

                    break;
                case PlayerInput:
                    PlayerInput.PlayerInputInstance.isEscapeOK = true;
                    PlayerInput.PlayerInputInstance.PlayerComponentOn();
                    break;
                case HPManager:
                    HPManager.Instance.enabled = true;
                    break;
            }
        }
    }
}
    