using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    public WeaponObj nowWeapon;
    //����������λ��
    public Transform weapenPos;

    // Update is called once per frame
    void Update()
    {
        //1.WS����ǰ��
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime);

        //2.AD������ת
        this.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * roundSpeed * Time.deltaTime);

        //3.������ҿ�����̨��ת
        tankHead.transform.Rotate( Input.GetAxis("Mouse X") * Vector3.up * headRoundSpeed * Time.deltaTime);


        //4.����
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    public override void Fire()
    {
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }
    public override void Dead()
    {
        //base.Dead();
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //���������� Ѫ��
        GamePanel.Instance.UpdateHP(this.maxHp, this.hp);
    }

    /// <summary>
    /// �л�����
    /// </summary>
    /// <param name="obj"></param>
    public void ChangeWeapon(GameObject weapon)
    {
        //ɾ����ǰӵ������
        if (nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }
        //�л�����
        //�������� �������ĸ����� ��������û����
        GameObject weaponObj = Instantiate(weapon, weapenPos, false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        //��������ӵ����
        nowWeapon.SetFather(this);

    }

}
