using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanelManager : MonoBehaviour
{
    public enum ViewKind
    {
        Title,
        Game
    }
    private static PanelManager instance;
    public static PanelManager Instance
    {
        get
        {
            if (instance == null)
            {
                PanelManager[] templates = FindObjectsOfType<PanelManager>();
                if (templates.Length > 0)
                {
                    instance = templates[0];
                    instance.enabled = true;
                    instance.gameObject.SetActive(true);
                }
            }
            return instance;
        }
    }

    [SerializeField] private TitlePanel titlePanel;

    [SerializeField] private GamePanel gamePanel;

    [NonSerialized]
    private readonly Dictionary<ViewKind, View> views = new Dictionary<ViewKind, View>();

    [NonSerialized]
    private ViewKind activeView = ViewKind.Title;

    public ViewKind ActiveView
    {
        get
        {
            return activeView;
        }
        set
        {
            ChangeActiveView(value);
        }
    }

    private void Awake()
    {
        instance = this;

        //// 시스템 설정을 로드
        //if (!FutureEducationSettings.LoadFromXml())
        //{
        //    FutureEducationSettings.SaveToXml();
        //}

        //// 사용자 설정을 로드
        //FutureEducationSettings.Load();


        // 모든 뷰를 딕셔너리에 넣은 후 숨김
        views.Add(ViewKind.Title, titlePanel);
        views.Add(ViewKind.Game, gamePanel);

        foreach (KeyValuePair<ViewKind, View> view in views)
        {
            if (view.Value != null)
            {
                view.Value.SetActive(false);
            }
        }
    }

    private void Start()
    {
        //if (signalWebSocketClient != null)
        //{
        //    signalWebSocketClient.WebSocketURL = FutureEducationSettings.SignalWebSocketUrl;
        //    signalWebSocketClient.OnConnect += SignalWebSocketClient_OnConnect;
        //    signalWebSocketClient.OnDisconnect += SignalWebSocketClient_OnDisconnect;
        //    signalWebSocketClient.OnReceiveText += SignalWebSocketClient_OnReceiveText;
        //    signalWebSocketClient.Connect();
        //}

        // 타이틀 화면에서 시작
        titlePanel.Show();
        activeView = ViewKind.Title; 
        //StartCoroutine(Initialize());
    }

    private void SignalWebSocketClient_OnConnect()
    {
   
    }

    private void SignalWebSocketClient_OnDisconnect()
    {
        StartCoroutine(Reconnect());
    }

    private void SignalWebSocketClient_OnReceiveText(string message)
    {
       
    }


    private IEnumerator Initialize()
    {
        yield return null;

        //if (signalWebSocketClient != null)
        //{
        //    while (signalWebSocketClient.Connecting)
        //    {
        //        yield return null;
        //    }

        //    if (!signalWebSocketClient.Connected)
        //    {
        //        int boxID = MessageBox.Show("웹소켓 서버에 연결할 수 없습니다.", "프로그램 종료", "무시하고 진행", "오류", 0f, ApplicationQuit);
        //        while (!signalWebSocketClient.Connected)
        //        {
        //            yield return null;
        //        }
        //        MessageBox.Close(boxID);
        //    }
        //}

        //if (FutureEducationSettings.KioskNumber == 0 && doorRemoteSerialPort != null)
        //{
        //    if (!doorRemoteSerialPort.IsOpen)
        //    {
        //        SendActiveViewStatus("Error");
        //        MessageBox.Show("시리얼 포트를 사용할 수 없습니다.", "프로그램 종료", "무시하고 진행", "오류", 0f, ApplicationQuit);
        //    }
        //}
    }

    private IEnumerator Reconnect()
    {
        //if (!signalWebSocketClient.Connected)
        //{
        //    int boxID = MessageBox.Show("웹소켓 서버와의 연결이 끊어졌습니다.\n다시 연결될 때까지 잠시만 기다려 주십시오.", "확인");
        //    while (!signalWebSocketClient.Connected)
        //    {
        //        yield return null;
        //    }
        //    MessageBox.Close(boxID);
        //}
        yield return null;
    }

    private void ApplicationQuit(bool isOK)
    {
        if (isOK)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }


    public void ChangeActiveView(ViewKind targetView)
    {
        if (activeView != targetView)
        {
            if (views.ContainsKey(activeView))
            {
                views[activeView].Hide();
            }
            activeView = targetView;
            if (views.ContainsKey(targetView))
            {
                views[targetView].Show();
            }
        }
    }
}
