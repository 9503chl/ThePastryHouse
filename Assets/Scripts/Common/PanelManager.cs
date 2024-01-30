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
        titlePanel.Show();
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
