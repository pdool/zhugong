using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoading : SceneBase {

    private UISlider mSlider;
    private UILabel mLable;
    protected override void OnInitSkin()
    {
        base.SetMainSkinPath("Game/UI/SceneLoading");
        base.OnInitSkin();

    }
    protected override void OnInitDone()
    {
        mSlider = skinTransform.Find("Slider").GetComponent<UISlider>();
        mLable = skinTransform.Find("progress").GetComponent<UILabel>();
        mSlider.value = 0;
        mLable.text = (mSlider.value * 100).ToString() + "%";
        StartCoroutine(Test());
    }
    /// <summary>
    /// 通过异步练习递归
    /// </summary>
    /// <returns></returns>

	 IEnumerator Test()
    {
        yield return 1;//   暂停帧
        mSlider.value += 0.01f;
        SetLabel(mSlider.value);
        if(mSlider.value < 1)
        {
            StartCoroutine(Test());
        }
        else
        {
            SceneMgr.Instance.SwitchScene(SceneType.SceneHome);
        }
        yield return new WaitForSeconds(0.2f);//    暂停秒
    }
    void SetLabel(float value)
    {
        if(mLable != null)
        {
            mLable.text = (mSlider.value * 100).ToString("f2") + "%";
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
