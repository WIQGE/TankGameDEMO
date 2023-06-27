using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    //����ʵ�������ӵ�����
    public GameObject bullet;

    //�ⲿ�����м�������λ��
    public Transform[] shootPos;

    //����ӵ����
    public TankBaseObj fatherObj;

    //����ӵ����
    public void SetFather(TankBaseObj fatherObj)
    {
        this.fatherObj = fatherObj;
    }


    public void Fire()
    {
        //����λ�ô�����Ӧ���ӵ�
        for (int i = 0; i < shootPos.Length; i++)
        {
            //�����ӵ�Ԥ����
            GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            //�����ӵ���ʲô
            BulletObj bulletObj = obj.GetComponent<BulletObj>();
            bulletObj.SetFather(fatherObj);
        }
    }
}
