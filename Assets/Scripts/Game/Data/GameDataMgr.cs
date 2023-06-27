using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 游戏数据管理类 是个单例模式对象
/// </summary>
public class GameDataMgr 
{
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance => instance;
    //音效数据对象
    public MusicData musicData;

    public RankList rankData;
    private GameDataMgr()
    {
        //可以初始化游戏数据
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "Music") as MusicData;
        //如果第一次进入游戏，那么所有的数据要么是false，要么是0
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
    //提供API给外部更改数据

    //提供一个在排行榜中 添加数据的方法
    public void AddRankInfo(string name, int score, float time)
    {
        rankData.list.Add(new RankInfo(name, score, time));
        //排序
        rankData.list.Sort((a, b) => a.time > b.time ? 1 : -1 );
        //排序后移除10条后的数据
        for (int i = rankData.list.Count - 1; i >= 10; i--)
        {
            rankData.list.RemoveAt(i);
        }
        //存储
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
