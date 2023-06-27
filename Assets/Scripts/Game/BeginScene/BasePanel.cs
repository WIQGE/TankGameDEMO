using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;

    public static T Instance => instance;

    private void Awake()
    {
        //��Awake�г�ʼ����ԭ��
        //���ű�ֻ�����һ��
        //����������ű����������ں��� Awake��
        //ֱ�Ӽ�¼������ Ψһ������ű�
        instance = this as T;
    }


    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
