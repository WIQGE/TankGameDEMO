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
        //�õ��Լ������Ķ����ϵ���ƵԴ
        audioSource = this.GetComponent<AudioSource>();

        ChangeVelue(GameDataMgr.Instance.musicData.bkValue);
        ChangeOpen(GameDataMgr.Instance.musicData.isOpenBK);
    }
    /// <summary>
    /// �������ִ�С
    /// </summary>
    /// <param name="value">����ֵ0~1</param>
    public void ChangeVelue(float value)
    {
        audioSource.volume = value;
    }
    /// <summary>
    /// ���ر�������
    /// </summary>
    /// <param name="isOpen">�Ƿ���</param>
    public void ChangeOpen(bool isOpen)
    {
        //����������ǲ�����
        audioSource.mute = !isOpen;
    }
 
}
