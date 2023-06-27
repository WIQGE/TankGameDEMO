using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    
    //关闭按钮
    public CustomGUIButton btnClose;

    //控件较多 拖的工作量太大 通过代码找   
    private List<CustomGUILabel> labName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labTime = new List<CustomGUILabel>();


    void Start()
    {
        //关闭按钮
        btnClose.clickEvent += () =>
        {
            //隐藏自己
            BeginPanel.Instance.ShowMe();
            HideMe();
        };
        for (int i = 1; i < 11; i++)
        {
            labName.Add(this.transform.Find("Name/LabUName" + i).GetComponent<CustomGUILabel>());
            labScore.Add(this.transform.Find("Score/LabScore" + i).GetComponent<CustomGUILabel>());
            labTime.Add(this.transform.Find("Time/LabTime" + i).GetComponent<CustomGUILabel>());
        }
        //PlayerPrefs.DeleteAll();
        //GameDataMgr.Instance.AddRankInfo("w", 100, 8432);
        //GameDataMgr.Instance.AddRankInfo("w", 100, 8432);
        //GameDataMgr.Instance.AddRankInfo("i", 10, 999);

        //初始化后失活自己
        HideMe();
    }

    public void UpdatePanelInfo()
    {
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        for (int i = 0; i < list.Count; i++)
        {
            //名字
            labName[i].content.text = list[i].name;
            //分数
            labScore[i].content.text = list[i].score.ToString();
            //时间 单位是s 换算一下
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            //得到小时
            if (time / 3600 > 0)
            {
                labTime[i].content.text += time / 3600 + "时";
            }
            if (time % 3600 > 60 || time / 3600 > 0)
            {
                labTime[i].content.text += (time % 3600) / 60 + "分";
            }
            labTime[i].content.text += time % 60 + "秒";
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }



}
