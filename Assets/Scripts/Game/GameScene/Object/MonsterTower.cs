using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : TankBaseObj
{
    //间隔时间
    public float fireOffsetTime = 1;
    private float nowTime = 0;

    //发射位置
    public Transform[] shootPos;

    //子弹预设体 关联
    public GameObject bulletObj;


    void Update()
    {
        //不停的累加时间
        nowTime += Time.deltaTime;
        //当时间超过间隔时间就开火
        if (nowTime >= fireOffsetTime)
        {
            Fire();
            nowTime = 0;
        }
    }
    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            //实例化几个子弹
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //设置子弹的拥有者 方便进行属性计算
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }
    public override void Wound(TankBaseObj other)
    {
        //重写不要base，避免相应的计算
        //base.Wound(other);
    }
}
