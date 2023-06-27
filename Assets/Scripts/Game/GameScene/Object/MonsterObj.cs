using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class MonsterObj : TankBaseObj
{
    //Ҫ��̹����������֮�������ƶ�
    //��ǰĿ���
    private Transform targetPos;
    //�����
    public Transform[] randomPos;


    //̹��Ҫһֱ�����Լ���Ŀ��
    public Transform lookAtTarget;


    //��Ŀ�굽��һ����Χ�ں� ���һ��ʱ�俪��һ��
    //�������
    public float fireDis = 30;

    public float fireOffsetTime = 3;
    private float nowTime = 0;
    //����λ��
    public Transform[] shootPos;
    //�ӵ�Ԥ����
    public GameObject bulletObj;

    //Ѫ����ͼ �������
    public Texture maxHpBK;
    public Texture hpBK;

    //��ʾѪ����ʱ��ʱ��
    private float showTime;

    //û��new ����Ϊ���ǽṹ�� ����new
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
        #region �����֮����ƶ�
        //����Ŀ���
        this.transform.LookAt(targetPos);
        //��ͣ�����Լ����泯��λ��
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //֪ʶ�� Vector3������һ���õ�������֮�����ķ���
        if (Vector3.Distance(this.transform.position, targetPos.position) < 0.05f)
        {
            //�������
            RandomPos();
        }
        #endregion

        #region �����Լ���Ŀ��
        
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
            //�����ӵ���ӵ���� ����������Լ���
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }

    public override void Dead()
    {
        base.Dead();
        //��������ʱ�ӷ�
        GamePanel.Instance.AddScore(10);
    }

    //���������Ѫ��UI
    private void OnGUI()
    {
        if (showTime > 0)
        {
            //��ͣ��ʱ
            showTime -= Time.deltaTime;
            //��Ѫ��
            //1.�ѹ���λ�û�����Ļλ��
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //2.��Ļλ�� ����GUIλ��
            screenPos.y = Screen.height - screenPos.y;
            //���ű���
            screenPos.z = (100 - screenPos.z) / 100;
            //3.�ٻ���
            //��ͼ
            maxHpRect.width = 100 * screenPos.z;
            maxHpRect.height = 10 * screenPos.z;
            maxHpRect.x = screenPos.x - maxHpRect.width / 2;
            maxHpRect.y = screenPos.y - 50;
            GUI.DrawTexture(maxHpRect, maxHpBK);


            hpRect.width = (float)hp / maxHp * 100f * screenPos.z;
            hpRect.height = 10 * screenPos.z;
            hpRect.x = screenPos.x - maxHpRect.width / 2;
            hpRect.y = screenPos.y - 50;
            //����Ѫ�������Ѫ���ٷֱ� ���������
            GUI.DrawTexture(hpRect, hpBK);
        }
        
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //������ʾѪ����ʱ��
        showTime = 3;

    }
}
