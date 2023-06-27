using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    public WeaponObj nowWeapon;
    //武器父对象位置
    public Transform weapenPos;

    // Update is called once per frame
    void Update()
    {
        //1.WS控制前进
        this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime);

        //2.AD控制旋转
        this.transform.Rotate(Input.GetAxis("Horizontal") * Vector3.up * roundSpeed * Time.deltaTime);

        //3.鼠标左右控制炮台旋转
        tankHead.transform.Rotate( Input.GetAxis("Mouse X") * Vector3.up * headRoundSpeed * Time.deltaTime);


        //4.开火
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
        //更新主界面 血条
        GamePanel.Instance.UpdateHP(this.maxHp, this.hp);
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    /// <param name="obj"></param>
    public void ChangeWeapon(GameObject weapon)
    {
        //删除当前拥有武器
        if (nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }
        //切换武器
        //创建武器 设置它的父对象 并且缩放没问题
        GameObject weaponObj = Instantiate(weapon, weapenPos, false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        //设置武器拥有者
        nowWeapon.SetFather(this);

    }

}
