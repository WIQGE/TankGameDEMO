using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        btnBegin.clickEvent += () =>
        {
            //切换场景
            SceneManager.LoadScene("GameScene");
        };
        btnSetting.clickEvent += () =>
        {
            //打开设置面板
            SettingPanel.Instance.ShowMe();
            HideMe();
        };
        btnQuit.clickEvent += () =>
        {
            //退出
            PlayerPrefs.DeleteAll();
            Application.Quit();
        };
        btnRank.clickEvent += () =>
        {
            //打开排行榜
            RankPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
