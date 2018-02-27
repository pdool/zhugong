using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMgr : MonoBehaviour {

	
	void Start () {
        SceneMgr.Instance.SwitchScene(SceneType.SceneLogin,"哈哈哈哈",22);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
