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
            //��������������С
            GameDataMgr.Instance.ChangeBKValue(value);
        };
        sliderSound.changeVale += (value) =>
        {
            //������Ч������С
            GameDataMgr.Instance.ChangeSoundValue(value);
        };
        togMusic.changeValue += (value) =>
        {
            //�������ֿ���
            GameDataMgr.Instance.OpenOrCloseMusic(value);
        };
        togSound.changeValue += (value) =>
        {
            //������Ч����
            GameDataMgr.Instance.OpenOrCloseSound(value);
        };

        btnClose.clickEvent += () =>
        {
            //�����Լ�
            
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
        //��������ϵ���Ϣ ���Ǹ�����Ч���ݸ���
        MusicData data = GameDataMgr.Instance.musicData;
        //�����������
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
