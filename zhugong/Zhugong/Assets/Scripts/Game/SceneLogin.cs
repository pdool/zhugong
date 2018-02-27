using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogin : SceneBase {

    private UIInput mInputAcc;
    private UIInput mInputPass;
    // Use this for initialization
    protected override void OnInitSkin()
    {
        base.SetMainSkinPath("Game/UI/SceneLogin");
        base.OnInitSkin();

    }
    protected override void OnInitDone()
    {
        base.OnInitDone();
        mInputAcc = skinTransform.Find("InputAcc").GetComponent<UIInput>();
        mInputPass = skinTransform.Find("InputPass").GetComponent<UIInput>();
        string s = (string)sceneArgs[0];
        int i = (int)sceneArgs[1];

        Debug.LogError(s + " ----------- " + i);
    }

    protected override void OnClick(GameObject click)
    {
        base.OnClick(click);
        ClickButton(click);
    }
    protected override void OnDestoryFront()
    {
        base.OnDestoryFront();
        Debug.Log("OnDestoryFront");
    }

    void ClickButton(GameObject click)
    {
        if (click.name.Equals("btnLogin"))
        {
            Debug.Log(string.Format("点击了登录 账号：{0} 密码：{1}",mInputAcc.value,mInputPass.value));

            SceneMgr.Instance.SwitchScene(SceneType.SceneLoading,"ssss");
        }else if (click.name.Equals("btnReg"))
        {
            Debug.Log(string.Format("点击了注册 账号：{0} 密码：{1}", mInputAcc.value, mInputPass.value));
        }

    }
	
}
