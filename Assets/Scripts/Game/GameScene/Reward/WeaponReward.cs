using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //有多个用于随机的武器预设体 
    public GameObject[] weapenObj;

    //获取特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int index = Random.Range(0, weapenObj.Length);
            //得到撞到自己的玩家身上挂载的脚本 命名切换武器
            PlayerObj player = other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weapenObj[index]);

            //播放获取武器特效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            //控制 获取音效
            AudioSource source = eff.GetComponent<AudioSource>();
            source.volume = GameDataMgr.Instance.musicData.soundValue;
            source.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject);
        }
    }
}
