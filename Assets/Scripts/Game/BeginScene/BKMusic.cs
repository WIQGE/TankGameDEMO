using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        //得到自己依附的对象上的音频源
        audioSource = this.GetComponent<AudioSource>();

        ChangeVelue(GameDataMgr.Instance.musicData.bkValue);
        ChangeOpen(GameDataMgr.Instance.musicData.isOpenBK);
    }
    /// <summary>
    /// 背景音乐大小
    /// </summary>
    /// <param name="value">音量值0~1</param>
    public void ChangeVelue(float value)
    {
        audioSource.volume = value;
    }
    /// <summary>
    /// 开关背景音乐
    /// </summary>
    /// <param name="isOpen">是否开启</param>
    public void ChangeOpen(bool isOpen)
    {
        //如果开启就是不禁音
        audioSource.mute = !isOpen;
    }
 
}
