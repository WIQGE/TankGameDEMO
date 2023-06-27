using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    //��̨
    public GameObject tankHead;

    public int atk;
    public int def;
    public int maxHp;
    public int hp;

    public int moveSpeed = 10;
    public int roundSpeed = 100;
    public int headRoundSpeed = 100;

    //������Ч ������ӦԤ���� 
    public GameObject deadEff;

    /// <summary>
    /// ������󷽷� ������д������Ϊ
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// �����˹��� �ܵ��˺�
    /// </summary>
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0)
            return;

            this.hp -= dmg;
        
        //�ж�Ѫ�� <= 0ʱ����
        if (this.hp <= 0)
        {
            this.hp = 0;
            this.Dead();
        }
    }

    /// <summary>
    /// ��Ѫ��<=0����Ӧ������
    /// </summary>
    public virtual void Dead()
    {
        //�������� Ӧ���ڳ������Ƴ�
        Destroy(this.gameObject);
        //������ʱ�� Ӧ�ò��Ŷ�Ӧ��Ч
        if (deadEff != null)
        {
            //ʵ��������ʱ��λ�úͽǶ�һ��������
            GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            //����Ч���� ��������Ч ���Կ����ڴ˴�����Ч�������Ҳ������
            AudioSource audioSoure = effObj.GetComponent<AudioSource>();
            //������Ч���� ���� ��Ч��С ���Ƿ񲥷�
            audioSoure.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSoure.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            //������� ��һû�й�ѡ Play on awake
            audioSoure.Play();
        }
    }

}
