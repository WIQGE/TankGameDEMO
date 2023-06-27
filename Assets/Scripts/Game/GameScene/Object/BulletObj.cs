using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float moveSpeed = 50;
    //谁发射的我
    public TankBaseObj fatherObj;

    public GameObject effObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward *  moveSpeed * Time.deltaTime);
    }

    //和别人碰撞触发时
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube") || 
            other.gameObject.CompareTag("Player") && fatherObj.gameObject.CompareTag("Monster") ||
            other.gameObject.CompareTag("Monster") && fatherObj.gameObject.CompareTag("Player"))
        {
            //判断受伤
            //得到碰撞到的对象身上是否有坦克脚本
            //用里氏替换原则 父类去获取
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            //判断对象是否挂载坦克脚本
            if (obj != null)
                obj.Wound(fatherObj);

            //子弹销毁时创建爆炸特效
            if (effObj != null)
            {
                GameObject eff = Instantiate(effObj, this.transform.position, this.transform.rotation);
                //改音效的音量和状态
                AudioSource audioS = eff.GetComponent<AudioSource>();
                audioS.volume = GameDataMgr.Instance.musicData.soundValue;
                audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }

            Destroy(this.gameObject);
        }
    }

    public void SetFather(TankBaseObj fatherObj)
    {
        this.fatherObj = fatherObj;
    }
}
