using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResMgr : MonoBehaviour {

    #region
    private static ResMgr mInstance;
    public static ResMgr GetInstance()
    {
        if(mInstance == null)
        {
            mInstance = new GameObject("_ResMgr").AddComponent<ResMgr>();
        }
        return mInstance;
    }
    ResMgr()
    {
        hashTable = new Hashtable();
    }
    /// <summary>
    /// 资源缓存集合
    /// </summary>
    private Hashtable hashTable;
    public T Load<T>(string path,bool cache)where T : UnityEngine.Object
    {
        if (hashTable.Contains(path))
        {
            return hashTable[path] as T;
        }


        T asset = Resources.Load<T>(path);
        if(asset == null)
        {
            Debug.LogError("资源不存在  " + path);
        }
        else
        {
            hashTable.Add(path,asset) ;
            Debug.LogError("对象缓存  " + path);
        }
        return asset;
    }
    #endregion

    public GameObject CreateGameObject(string path,bool cache)
    {
        GameObject assetObj = Load<GameObject>(path, cache);
        GameObject go = Instantiate(assetObj);
        if(go  == null)
        {
            Debug.LogError("从Res中创建游戏对象失败 "+ path);
        }
        return go;
    }
}
