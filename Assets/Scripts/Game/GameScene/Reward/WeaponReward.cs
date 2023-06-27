using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //�ж���������������Ԥ���� 
    public GameObject[] weapenObj;

    //��ȡ��Ч
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int index = Random.Range(0, weapenObj.Length);
            //�õ�ײ���Լ���������Ϲ��صĽű� �����л�����
            PlayerObj player = other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weapenObj[index]);

            //���Ż�ȡ������Ч
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            //���� ��ȡ��Ч
            AudioSource source = eff.GetComponent<AudioSource>();
            source.volume = GameDataMgr.Instance.musicData.soundValue;
            source.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject);
        }
    }
}
