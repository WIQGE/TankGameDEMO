using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    //加属性的4种类型
    Atk,
    Def,
    MaxHp,
    Hp,
}

public class PropReward : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;

    public int changeValue = 2;

    //获取特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //玩家才能获取属性奖励
        if(other.CompareTag("Player"))
        {
            //得到对应玩家脚本
            PlayerObj playerObj = other.GetComponent<PlayerObj>();
            //根据类型加属性
            switch (type)
            {
                case E_PropType.Atk:
                    playerObj.atk += changeValue;
                    break;
                case E_PropType.Def:
                    playerObj.def += changeValue;
                    break;
                case E_PropType.MaxHp:
                    playerObj.maxHp += changeValue;
                    //更新血条
                    GamePanel.Instance.UpdateHP(playerObj.maxHp, playerObj.hp);
                    break;
                case E_PropType.Hp:
                    playerObj.hp += changeValue;
                    if (playerObj.hp > playerObj.maxHp)
                    {
                        playerObj.hp = playerObj.maxHp;
                    }
                    //更新血条
                    GamePanel.Instance.UpdateHP(playerObj.maxHp, playerObj.hp);
                    break;
            }

            //创建特效 设置音效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            AudioSource source = eff.GetComponent<AudioSource>();
            source.volume = GameDataMgr.Instance.musicData.soundValue;
            source.mute = !GameDataMgr.Instance.musicData.isOpenSound;


            Destroy(this.gameObject);
        }
    }
}
