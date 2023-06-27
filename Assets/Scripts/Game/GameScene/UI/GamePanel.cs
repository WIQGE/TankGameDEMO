using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    //获取控件 得分、时间
    public CustomGUILabel labScore;
    public CustomGUILabel labTime;
    //退出、设置按钮
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;
    //血条
    public CustomGUITexture texHP;
    //血条控件的宽
    public float hpw = 350;

    [HideInInspector]
    public int nowScore = 0;
    [HideInInspector]
    public float nowTime = 0;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        //监听界面上的一些控件
        btnSetting.clickEvent += () =>
        {
            //设置面板打开逻辑
            Time.timeScale = 0;
            SettingPanel.Instance.ShowMe();
            HideMe();
        };
        btnQuit.clickEvent += () =>
        {
            //退出游戏场景打开开始场景逻辑
            //最好有弹一个确认界面出来
            Time.timeScale = 0;
            QuitPanel.Instance.ShowMe();
            HideMe();
        };

        //AddScore(100);
        //UpdateHP(100, 60);
    }

    // Update is called once per frame
    void Update()
    {
        //通过帧间隔时间 进行累加比较准确
        nowTime += Time.deltaTime;
        //把秒转换成时分秒
        time = (int)nowTime;
        labTime.content.text = "";
        //得到小时
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "时";
        }
        if (time % 3600 > 60 || time / 3600 > 0)
        {
            labTime.content.text += (time % 3600) / 60 + "分";
        }
        labTime.content.text += time % 60 + "秒";
    }

    /// <summary>
    /// 提供给外部的加分方法
    /// </summary>
    /// <param name="Score"></param>
    public void AddScore(int Score)
    {
        nowScore += Score;
        labScore.content.text = Score.ToString();
    }

    /// <summary>
    /// 更新血条的方法
    /// </summary>
    /// <param name="maxHP">最大血量</param>
    /// <param name="HP">当前血量</param>
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.guiPos.width = (float)HP / maxHP * hpw;
    }
}
