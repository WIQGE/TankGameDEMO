using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    //关联控件
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnSure;

    private void Start()
    {
        btnSure.clickEvent += () =>
        {
            Time.timeScale = 1;

            //把数据记录到排行榜中
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text, 
                GamePanel.Instance.nowScore, 
                GamePanel.Instance.nowTime);
            //回到开始场景中
            SceneManager.LoadScene("BeginScene");
        };

        HideMe();
    }

}
