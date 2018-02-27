using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMgr {
    #region 初始化
    protected static PanelMgr mInstance;
    public static PanelMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new PanelMgr();
            }
            return mInstance;
        }

    }
    private PanelMgr()
    {
        panels = new Dictionary<PanelType, PanelBase>();

    }
    void Destroy()
    {
        panels.Clear();
        panels = null;
    }
    #endregion


    #region 数据定义
    public enum PanelShowStyle
    {
        /// <summary>
        /// 
        /// </summary>
        Nomal,
        CenterScaleBigNomal,
        UpToSlide,
        DownToSlide,
        LeftToSlide,
        RightToSlide,
    }

    public enum PanelMaskStyle
    {
        None,
        BlackAlpha,
        Alpha,
    }


    public Dictionary<PanelType, PanelBase> panels; 
    #endregion
    private Transform parentObj = null;
    /// <summary>当前打开的面板 </summary>
    private PanelBase current;
    public void ShowPanel(PanelType panelType, params object[] sceneArgs)
    {
        if (panels.ContainsKey(panelType))
        {
            current = panels[panelType];
            current.gameObject.SetActive(false);
            current.Init();
        }
        else
        {
            string name = panelType.ToString();
            GameObject scene = new GameObject(name);
            current = scene.AddComponent(Type.GetType(name)) as PanelBase;
            //baseObj.Init(sceneArgs);
            current.gameObject.SetActive(false);
            current.Init();
            panels.Add(panelType,current);
            if (parentObj == null)
            {
                parentObj = GameObject.Find("UI Root").transform;
            }
            scene.transform.parent = parentObj;
            scene.transform.localEulerAngles = Vector3.zero;
            scene.transform.localScale = Vector3.one;
            scene.transform.localPosition = Vector3.zero;

            LayerMgr.GetInstance().SetLayer(current.gameObject,LayerType.Panel);
        }
        StartShowPanel(current, current.showStyle, true);
    }
    public void HidePanel(PanelType panelType)
    {
        if (panels.ContainsKey(panelType))
        {
            PanelBase pb = panels[panelType];
            StartShowPanel(pb, pb.showStyle, false);
        }
        else
        {
            Debug.LogError("xxxxxxxxxxx");
        }
    }
    /// <summary>
    /// 强制关闭面板
    /// </summary>
    /// <param name="panelType"></param>
    public void DestroyPanel(PanelType panelType)
    {
        if (panels.ContainsKey(panelType))
        {
            PanelBase pb = panels[panelType];
            GameObject.Destroy(pb.gameObject);
            panels.Remove(panelType);
        }
    }
    private void StartShowPanel(PanelBase go,PanelShowStyle showStyle,bool isOpen)
    {
        switch (showStyle)
        {
            case PanelShowStyle.Nomal:
                ShowNormal(go, isOpen);
                break;
            case PanelShowStyle.CenterScaleBigNomal:
                ShowCenterScaleBigNomal(go,isOpen);
                break;
            case PanelShowStyle.UpToSlide:
                ShowUpToSlide(go,isOpen,true);
                break;
            case PanelShowStyle.DownToSlide:
                ShowUpToSlide(go, isOpen, false);
                break;
            case PanelShowStyle.LeftToSlide:
                ShowLeftToSlide(go, isOpen,true);
                break;
            case PanelShowStyle.RightToSlide:
                ShowLeftToSlide(go, isOpen, false);
                break;
        }
    }



    #region 各种打开效果

    void ShowNormal(PanelBase go,bool isOpen)
    {
        if (!isOpen)
        {
            DestroyPanel(go.panelType);
        }else
        {
            go.gameObject.SetActive(true);
        }
    }

    private void ShowCenterScaleBigNomal(PanelBase go,bool isOpen)
    {
        TweenScale ts = go.gameObject.GetComponent<TweenScale>();
        if(ts == null) ts = go.gameObject.AddComponent<TweenScale>();
        ts.from = Vector3.zero;
        ts.to = Vector3.one;
        ts.duration = 0.2f;
        ts.SetOnFinished(() =>{
            if (!isOpen)
            {
                DestroyPanel(go.panelType);
            }
        });
        go.gameObject.SetActive(true);
        if (!isOpen) ts.Play(isOpen);
    }
    /// <summary>
    /// 左右往中间
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isOpen"></param>
    /// <param name="isLeft"></param>
    private void ShowLeftToSlide(PanelBase go, bool isOpen, bool isLeft)
    {
        TweenPosition tp = go.gameObject.GetComponent<TweenPosition>();
        if (tp == null) tp = go.gameObject.AddComponent<TweenPosition>();
        tp.from = isLeft ? new Vector3(-700,0,0): new Vector3( 700,0, 0);
        tp.to = Vector3.one;
        tp.duration = go.openDuration;
        tp.SetOnFinished(() =>
        {
            if (!isOpen)
            {
                DestroyPanel(go.panelType);
            }
        });
        go.gameObject.SetActive(true);
        if (!isOpen) tp.Play(isOpen);
    }


    private void ShowUpToSlide(PanelBase go, bool isOpen,bool isUp)
    {
        TweenPosition tp = go.gameObject.GetComponent<TweenPosition>();
        if (tp == null) tp = go.gameObject.AddComponent<TweenPosition>();
        tp.from = isUp ? new Vector3( 0,700, 0) : new Vector3( 0,-700, 0);
        tp.to = Vector3.one;
        tp.duration = go.openDuration;
        tp.SetOnFinished(() =>
        {
            if (!isOpen)
            {
                DestroyPanel(go.panelType);
            }
        });
        go.gameObject.SetActive(true);
        if (!isOpen) tp.Play(isOpen);
    }

    #endregion

}


