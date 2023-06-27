using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float moveSpeed = 50;
    //˭�������
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

    //�ͱ�����ײ����ʱ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube") || 
            other.gameObject.CompareTag("Player") && fatherObj.gameObject.CompareTag("Monster") ||
            other.gameObject.CompareTag("Monster") && fatherObj.gameObject.CompareTag("Player"))
        {
            //�ж�����
            //�õ���ײ���Ķ��������Ƿ���̹�˽ű�
            //�������滻ԭ�� ����ȥ��ȡ
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            //�ж϶����Ƿ����̹�˽ű�
            if (obj != null)
                obj.Wound(fatherObj);

            //�ӵ�����ʱ������ը��Ч
            if (effObj != null)
            {
                GameObject eff = Instantiate(effObj, this.transform.position, this.transform.rotation);
                //����Ч��������״̬
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
