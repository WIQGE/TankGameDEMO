using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class MonsterObj : TankBaseObj
{
    //要让坦克在两个点之间来回移动
    //当前目标点
    private Transform targetPos;
    //随机点
    public Transform[] randomPos;


    //坦克要一直盯着自己的目标
    public Transform lookAtTarget;


    //当目标到达一定范围内后 间隔一段时间开火一次
    //开火距离
    public float fireDis = 30;

    public float fireOffsetTime = 3;
    private float nowTime = 0;
    //发射位置
    public Transform[] shootPos;
    //子弹预设体
    public GameObject bulletObj;

    //血条的图 外面关联
    public Texture maxHpBK;
    public Texture hpBK;

    //显示血条计时用时间
    private float showTime;

    //没有new 是因为他是结构体 不用new
    private Rect maxHpRect;
    private Rect hpRect;

    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        #region 多个点之间的移动
        //看向目标点
        this.transform.LookAt(targetPos);
        //不停的向自己的面朝向位移
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //知识点 Vector3里面有一个得到两个点之间距离的方法
        if (Vector3.Distance(this.transform.position, targetPos.position) < 0.05f)
        {
            //重新随机
            RandomPos();
        }
        #endregion

        #region 看向自己的目标
        
        if (lookAtTarget != null)
        {
            if (Vector3.Distance(this.transform.position, lookAtTarget.position) <= fireDis)
            {
                tankHead.transform.LookAt(lookAtTarget);
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffsetTime)
                {
                    Fire();
                    nowTime = 0;
                }
            }
            
        }
        #endregion


    }
    private void RandomPos()
    {
        if (randomPos == null)
        {
            return;
        }
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
    }

    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            //设置子弹的拥有者 方便进行属性计算
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
        //怪物死亡时加分
        GamePanel.Instance.AddScore(10);
    }

    //在这里绘制血条UI
    private void OnGUI()
    {
        if (showTime > 0)
        {
            //不停计时
            showTime -= Time.deltaTime;
            //画血条
            //1.把怪物位置换成屏幕位置
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //2.屏幕位置 换成GUI位置
            screenPos.y = Screen.height - screenPos.y;
            //缩放比例
            screenPos.z = (100 - screenPos.z) / 100;
            //3.再绘制
            //底图
            maxHpRect.width = 100 * screenPos.z;
            maxHpRect.height = 10 * screenPos.z;
            maxHpRect.x = screenPos.x - maxHpRect.width / 2;
            maxHpRect.y = screenPos.y - 50;
            GUI.DrawTexture(maxHpRect, maxHpBK);


            hpRect.width = (float)hp / maxHp * 100f * screenPos.z;
            hpRect.height = 10 * screenPos.z;
            hpRect.x = screenPos.x - maxHpRect.width / 2;
            hpRect.y = screenPos.y - 50;
            //根据血量和最大血量百分比 决定画多宽
            GUI.DrawTexture(hpRect, hpBK);
        }
        
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //设置显示血条的时间
        showTime = 3;

    }
}
