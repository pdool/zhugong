using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMgr :MonoBehaviour{
    #region 初始化
    private static LayerMgr mInstance;
    public static LayerMgr GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new GameObject("_LayerMgr").AddComponent<LayerMgr>();
        }
        return mInstance;
    }
    #endregion

    private LayerMgr()
    {
        mLayerDict = new Dictionary<LayerType, GameObject>();
    }

    private Dictionary<LayerType, GameObject> mLayerDict;
    private Transform mParent;

    public void SetLayer(GameObject current,LayerType layerType)
    {
        if (mLayerDict.Count < Enum.GetNames(typeof(LayerType)).Length)
        {
            //层次初始化
            LayerInit();
        }
        current.transform.parent = mLayerDict[layerType].transform;
        UIPanel[] panels = current.GetComponentsInChildren<UIPanel>(true);
        foreach(UIPanel panel in panels)
        {
            panel.depth += (int)layerType;
        }
    }


    private void LayerInit()
    {
        mParent = GameObject.Find("UI Root").transform;

        int nums = Enum.GetNames(typeof(LayerType)).Length;

        for(int i = 0; i < nums; i++)
        {
            object obj = Enum.GetValues(typeof(LayerType)).GetValue(i);
            mLayerDict.Add((LayerType)obj, CreateLayerGameObject(obj.ToString(), (LayerType)obj));
        }


    }


    private GameObject CreateLayerGameObject(string name,LayerType layerType)
    {
        GameObject layer = new GameObject(name);
        layer.transform.parent = mParent;
        layer.transform.localEulerAngles = Vector3.zero;
        layer.transform.localScale = Vector3.one;
        layer.transform.localPosition = new Vector3(0,0,(int)layerType * -1);
        return layer;
    }


}
public enum LayerType
{
    Scene = 50,
    Panel = 200,
    Tips = 400,
    Notice = 1000,
}