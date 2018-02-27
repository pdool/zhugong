using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChat : SceneBase {

    protected override void OnInitSkin()
    {
        base.SetMainSkinPath("Game/UI/Chat/SceneChat");
        base.OnInitSkin();

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
    public override void OnResetArgs(params object[] sceneArgs)
    {
        base.OnResetArgs(sceneArgs);
    }



    #region 点击事件
    void ClickButton(GameObject click)
    {
        if (click.name.Equals("BtnMail"))
        {
            Debug.LogWarning("打开邮件");
            SceneMgr.Instance.SwitchScene(SceneType.SceneMail);
        }
        else if (click.name.Equals("BtnHeadIcon"))
        {
            PanelMgr.Instance.ShowPanel(PanelType.PanelPlayerInfo);
        }
    }
    #endregion
}
