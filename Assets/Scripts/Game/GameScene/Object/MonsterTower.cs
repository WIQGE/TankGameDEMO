using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTower : TankBaseObj
{
    //���ʱ��
    public float fireOffsetTime = 1;
    private float nowTime = 0;

    //����λ��
    public Transform[] shootPos;

    //�ӵ�Ԥ���� ����
    public GameObject bulletObj;


    void Update()
    {
        //��ͣ���ۼ�ʱ��
        nowTime += Time.deltaTime;
        //��ʱ�䳬�����ʱ��Ϳ���
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
            //ʵ���������ӵ�
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //�����ӵ���ӵ���� ����������Լ���
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }
    public override void Wound(TankBaseObj other)
    {
        //��д��Ҫbase��������Ӧ�ļ���
        //base.Wound(other);
    }
}
