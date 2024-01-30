using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitPanel : View
{
    private void Awake()
    {
        OnAfterShow += ExitPanel_OnAfterShow;
        OnBeforeShow += ExitPanel_OnBeforeShow;
    }

    private void ExitPanel_OnBeforeShow()
    {

    }
    private void ExitPanel_OnAfterShow()
    {

    }
}
