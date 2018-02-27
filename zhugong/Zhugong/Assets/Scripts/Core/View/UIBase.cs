using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour {


    #region 初始化相关


    void Start()
    {
        OnStart();
    }
    void Update()
    {
        OnUpdate();
    }
    #endregion
    private GameObject _skin;
    public GameObject skin
    {
        get
        {
            return _skin;
        }
    }
    public Transform skinTransform
    {
        get
        {
            return _skin.transform;
        }
    }
    private string mainSkinPath;
    protected void SetMainSkinPath(string path)
    {
        mainSkinPath = path;
    }

    private List<Collider> colliderList = new List<Collider>();
    public void Init()
    {
        OnInit();
        OnInitSkin();
        OnInitSkinDone();
        Collider[] colliders = this.GetComponentsInChildren<Collider>(true);


        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            UIEventListener listener = UIEventListener.Get(collider.gameObject);
            listener.onClick = OnClick;
            colliderList.Add(collider);
        }
        OnInitDone();
    }

    public void OnDestroy()
    {
        OnDestoryFront();
        colliderList.Clear();
        colliderList = null;
        OnDestoryEnd();
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }

    public virtual void OnInit() { }
    protected virtual void OnInitDone() { }
    /// <summary>
    /// 开始删除
    /// </summary>
    protected virtual void OnDestoryFront() { }
    /// <summary>
    /// 删除完成
    /// </summary>
    protected virtual void OnDestoryEnd() { }
    protected virtual void OnClick(GameObject click) { }

    protected virtual void OnInitSkin()
    {
        if (!string.IsNullOrEmpty(mainSkinPath))
        {
            _skin = ResMgr.GetInstance().CreateGameObject(mainSkinPath, false);
        }
        _skin.transform.parent = this.transform;
        _skin.transform.localEulerAngles = Vector3.zero;
        _skin.transform.localPosition = Vector3.zero;
        _skin.transform.localScale = Vector3.one;
    }
    protected virtual void OnInitSkinDone() { }
}
