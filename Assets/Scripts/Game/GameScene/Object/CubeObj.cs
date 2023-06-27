using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    public int probability = 50;
    public GameObject[] rewardObjs;

    public GameObject deadEff;

    private void OnTriggerEnter(Collider other)
    {
        int rangeInt = Random.Range(0, 100);
        if (rangeInt < probability)
        {
            //随机创建一个 奖励预设体
            rangeInt = Random.Range(0, rewardObjs.Length);
            Instantiate(rewardObjs[rangeInt],this.transform.position,this.transform.rotation);
        }

        GameObject effObj = Instantiate(deadEff,transform.position,this.transform.rotation);
        AudioSource source = effObj.GetComponent<AudioSource>();
        source.volume = GameDataMgr.Instance.musicData.soundValue;
        source.mute = !GameDataMgr.Instance.musicData.isOpenSound;


        Destroy(gameObject);
    }
}
