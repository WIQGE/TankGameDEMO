using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    //�����Ե�4������
    Atk,
    Def,
    MaxHp,
    Hp,
}

public class PropReward : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;

    public int changeValue = 2;

    //��ȡ��Ч
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //��Ҳ��ܻ�ȡ���Խ���
        if(other.CompareTag("Player"))
        {
            //�õ���Ӧ��ҽű�
            PlayerObj playerObj = other.GetComponent<PlayerObj>();
            //�������ͼ�����
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
                    //����Ѫ��
                    GamePanel.Instance.UpdateHP(playerObj.maxHp, playerObj.hp);
                    break;
                case E_PropType.Hp:
                    playerObj.hp += changeValue;
                    if (playerObj.hp > playerObj.maxHp)
                    {
                        playerObj.hp = playerObj.maxHp;
                    }
                    //����Ѫ��
                    GamePanel.Instance.UpdateHP(playerObj.maxHp, playerObj.hp);
                    break;
            }

            //������Ч ������Ч
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            AudioSource source = eff.GetComponent<AudioSource>();
            source.volume = GameDataMgr.Instance.musicData.soundValue;
            source.mute = !GameDataMgr.Instance.musicData.isOpenSound;


            Destroy(this.gameObject);
        }
    }
}
