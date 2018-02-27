using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : UIBase
{

    protected PanelType _panelType;
    public PanelType panelType
    {
        get
        {
            return _panelType;
        }
    }
    protected float _openDuration = 0.2f;
    public float openDuration
    {
        get
        {
            return _openDuration;
        }
    }
    protected PanelMgr.PanelShowStyle _showStyle = PanelMgr.PanelShowStyle.CenterScaleBigNomal;

    /// <summary>面板显示方式 </summary>
    public PanelMgr.PanelShowStyle showStyle
    {
        get
        {
            return _showStyle;
        }
    }

    protected void Close()
    {
        Destroy(gameObject);
    }


}
public enum PanelType
{
    PanelPlayerInfo,
}