using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    //��ȡ�ؼ� �÷֡�ʱ��
    public CustomGUILabel labScore;
    public CustomGUILabel labTime;
    //�˳������ð�ť
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;
    //Ѫ��
    public CustomGUITexture texHP;
    //Ѫ���ؼ��Ŀ�
    public float hpw = 350;

    [HideInInspector]
    public int nowScore = 0;
    [HideInInspector]
    public float nowTime = 0;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        //���������ϵ�һЩ�ؼ�
        btnSetting.clickEvent += () =>
        {
            //���������߼�
            Time.timeScale = 0;
            SettingPanel.Instance.ShowMe();
            HideMe();
        };
        btnQuit.clickEvent += () =>
        {
            //�˳���Ϸ�����򿪿�ʼ�����߼�
            //����е�һ��ȷ�Ͻ������
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
        //ͨ��֡���ʱ�� �����ۼӱȽ�׼ȷ
        nowTime += Time.deltaTime;
        //����ת����ʱ����
        time = (int)nowTime;
        labTime.content.text = "";
        //�õ�Сʱ
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "ʱ";
        }
        if (time % 3600 > 60 || time / 3600 > 0)
        {
            labTime.content.text += (time % 3600) / 60 + "��";
        }
        labTime.content.text += time % 60 + "��";
    }

    /// <summary>
    /// �ṩ���ⲿ�ļӷַ���
    /// </summary>
    /// <param name="Score"></param>
    public void AddScore(int Score)
    {
        nowScore += Score;
        labScore.content.text = Score.ToString();
    }

    /// <summary>
    /// ����Ѫ���ķ���
    /// </summary>
    /// <param name="maxHP">���Ѫ��</param>
    /// <param name="HP">��ǰѪ��</param>
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.guiPos.width = (float)HP / maxHP * hpw;
    }
}
