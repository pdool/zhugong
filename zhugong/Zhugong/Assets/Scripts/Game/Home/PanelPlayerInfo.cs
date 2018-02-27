using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPlayerInfo : PanelBase {
    protected override void OnInitSkin()
    {
        base.SetMainSkinPath("Game/UI/Home/PanelPlayerInfo");
        base.OnInitSkin();
        base._showStyle = PanelMgr.PanelShowStyle.UpToSlide;
        base._panelType = PanelType.PanelPlayerInfo;

    }
    protected override void OnInitDone()
    {
        base.OnInitDone();

    }
    protected override void OnClick(GameObject click)
    {
        base.OnClick(click);
        ClickButton(click);
    }

    void ClickButton(GameObject click)
    {
        if (click.name.Equals("BtnClose"))
        {
            PanelMgr.Instance.HidePanel(panelType);
        }
    }
    protected void CloseImmediate()
    {
        PanelMgr.Instance.DestroyPanel(panelType);
    }
}
