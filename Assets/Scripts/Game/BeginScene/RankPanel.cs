using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    
    //�رհ�ť
    public CustomGUIButton btnClose;

    //�ؼ��϶� �ϵĹ�����̫�� ͨ��������   
    private List<CustomGUILabel> labName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labTime = new List<CustomGUILabel>();


    void Start()
    {
        //�رհ�ť
        btnClose.clickEvent += () =>
        {
            //�����Լ�
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

        //��ʼ����ʧ���Լ�
        HideMe();
    }

    public void UpdatePanelInfo()
    {
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        for (int i = 0; i < list.Count; i++)
        {
            //����
            labName[i].content.text = list[i].name;
            //����
            labScore[i].content.text = list[i].score.ToString();
            //ʱ�� ��λ��s ����һ��
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            //�õ�Сʱ
            if (time / 3600 > 0)
            {
                labTime[i].content.text += time / 3600 + "ʱ";
            }
            if (time % 3600 > 60 || time / 3600 > 0)
            {
                labTime[i].content.text += (time % 3600) / 60 + "��";
            }
            labTime[i].content.text += time % 60 + "��";
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }



}
