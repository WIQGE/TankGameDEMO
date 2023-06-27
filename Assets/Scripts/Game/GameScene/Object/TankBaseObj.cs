using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    //炮台
    public GameObject tankHead;

    public int atk;
    public int def;
    public int maxHp;
    public int hp;

    public int moveSpeed = 10;
    public int roundSpeed = 100;
    public int headRoundSpeed = 100;

    //死亡特效 关联对应预设体 
    public GameObject deadEff;

    /// <summary>
    /// 开火抽象方法 子类重写开火行为
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// 被别人攻击 受到伤害
    /// </summary>
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0)
            return;

            this.hp -= dmg;
        
        //判断血量 <= 0时死亡
        if (this.hp <= 0)
        {
            this.hp = 0;
            this.Dead();
        }
    }

    /// <summary>
    /// 当血量<=0死就应该死亡
    /// </summary>
    public virtual void Dead()
    {
        //对象死亡 应该在场景上移除
        Destroy(this.gameObject);
        //死亡的时候 应该播放对应特效
        if (deadEff != null)
        {
            //实例化对象时把位置和角度一起设置了
            GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            //该特效身上 关联了音效 所以可以在此处把音效播放相关也控制了
            AudioSource audioSoure = effObj.GetComponent<AudioSource>();
            //根据音效数据 设置 音效大小 和是否播放
            audioSoure.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSoure.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            //保险起见 万一没有勾选 Play on awake
            audioSoure.Play();
        }
    }

}
