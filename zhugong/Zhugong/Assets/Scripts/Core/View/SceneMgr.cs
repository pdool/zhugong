using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr {
 
    protected static SceneMgr mInstance;
    public static SceneMgr Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new SceneMgr();
            }
            return mInstance;
        }
        
    }

    private GameObject current;

    private Transform parentObj = null;

    private List<SwitchRecorder> switchRecorder;

    private SceneMgr()
    {
        switchRecorder = new List<SwitchRecorder>();
    }
    public void OnDestroy()
    {
        switchRecorder.Clear();
        switchRecorder = null;
    }

    public void SwitchScene(SceneType sceneType, params object[] sceneArgs)
    {
        string name = sceneType.ToString();
        GameObject scene = new GameObject(name);
        SceneBase baseObj = scene.AddComponent(Type.GetType(name)) as SceneBase;
        //baseObj.Init(sceneArgs);
        baseObj.OnInit(sceneArgs);
        if(parentObj != null)
        {
            parentObj = GameObject.Find("UI Root").transform;
        }
      scene.transform.parent = parentObj;


        LayerMgr.GetInstance().SetLayer(baseObj.gameObject, LayerType.Scene);
        
        scene.transform.localEulerAngles = Vector3.zero;
        scene.transform.localScale = Vector3.one;
        scene.transform.localPosition = Vector3.zero;
        if (name.Equals("SceneHome"))
        {
            switchRecorder.Clear();
        }

        switchRecorder.Add(new SwitchRecorder(sceneType, sceneArgs));
        if (current != null)
        {
            GameObject.Destroy(current);
        }
 
        current = scene;
    }
    public void SwitchToPrevScene()
    {
        SwitchRecorder sr = switchRecorder[switchRecorder.Count - 2];
        switchRecorder.RemoveRange(switchRecorder.Count - 2,2);
        SwitchScene(sr.sceneType, sr.sceneArgs);
    }


    internal struct SwitchRecorder
    {
        internal SceneType sceneType;
        internal object[] sceneArgs;
        internal SwitchRecorder(SceneType sceneType, params object[] sceneArgs)
        {
            this.sceneType = sceneType;
            this.sceneArgs = sceneArgs;
        }
    }
}
