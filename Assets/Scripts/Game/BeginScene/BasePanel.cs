using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;

    public static T Instance => instance;

    private void Awake()
    {
        //在Awake中初始化的原因
        //面板脚本只会挂载一次
        //可以在这个脚本的生命周期函数 Awake中
        //直接记录场景上 唯一的这个脚本
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
