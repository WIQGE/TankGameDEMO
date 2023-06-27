using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;

    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIButton btnClose;

    private void Start()
    {
        sliderMusic.changeVale += (value) =>
        {
            //处理音乐声音大小
            GameDataMgr.Instance.ChangeBKValue(value);
        };
        sliderSound.changeVale += (value) =>
        {
            //处理音效声音大小
            GameDataMgr.Instance.ChangeSoundValue(value);
        };
        togMusic.changeValue += (value) =>
        {
            //处理音乐开关
            GameDataMgr.Instance.OpenOrCloseMusic(value);
        };
        togSound.changeValue += (value) =>
        {
            //处理音效开关
            GameDataMgr.Instance.OpenOrCloseSound(value);
        };

        btnClose.clickEvent += () =>
        {
            //隐藏自己
            
            if (SceneManager.GetActiveScene().name == "BeginScene") 
            {
                BeginPanel.Instance.ShowMe();
            }
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                GamePanel.Instance.ShowMe();
            }
            
            HideMe();
        };

        HideMe();
    }

    public void UpdatePanelInfo()
    {
        //我们面板上的信息 都是根据音效数据更新
        MusicData data = GameDataMgr.Instance.musicData;
        //设置面板内容
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenBK;
        togSound.isSel = data.isOpenSound;
    }

    public override void ShowMe()
    {
        base.ShowMe();

        UpdatePanelInfo();
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }

}
