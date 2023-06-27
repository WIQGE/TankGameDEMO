using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��Ϸ���ݹ����� �Ǹ�����ģʽ����
/// </summary>
public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance => instance;
    //��Ч���ݶ���
    public MusicData musicData;

    public RankList rankData;
    private GameDataMgr()
    {
        //���Գ�ʼ����Ϸ����
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "Music") as MusicData;
        //�����һ�ν�����Ϸ����ô���е�����Ҫô��false��Ҫô��0
        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.isOpenBK = true;
            musicData.isOpenSound = true;
            musicData.bkValue = 1;
            musicData.soundValue = 1;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
        }
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "Rank") as RankList;
    }
    //�ṩAPI���ⲿ��������

    //�ṩһ�������а��� ������ݵķ���
    public void AddRankInfo(string name, int score, float time)
    {
        rankData.list.Add(new RankInfo(name, score, time));
        //����
        rankData.list.Sort((a, b) => a.time > b.time ? 1 : -1 );
        //������Ƴ�10���������
        for (int i = rankData.list.Count - 1; i >= 10; i--)
        {
            rankData.list.RemoveAt(i);
        }
        //�洢
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "Rank");
    }
    public void OpenOrCloseMusic(bool isOpen)
    {
        musicData.isOpenBK = isOpen;
        BKMusic.Instance.ChangeOpen(isOpen);

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }
    public void OpenOrCloseSound(bool isOpen)
    {
        musicData.isOpenSound = isOpen;

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }
    public void ChangeBKValue(float value)
    {
        musicData.bkValue = value;
        BKMusic.Instance.ChangeVelue(value);

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }
    public void ChangeSoundValue(float value)
    {
        musicData.soundValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "Music");
    }
    


}
