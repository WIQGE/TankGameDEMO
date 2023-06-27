using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对齐方式枚举
/// </summary>
public enum E_Alignment_Type
{
    Up, 
    Down,
    Left,
    Right,
    Center,
    Left_Up,
    Left_Down,
    Right_Up,
    Right_Down,

}

[System.Serializable]
/// <summary>
/// 该类用于表示位置 计算位置相关 不需要继承Mono
/// </summary>
public class CustomGUIPos 
{
    //主要处理 控件位置相关
    //要完成 自适应分辨率的相关计算

    //该位置信息 会用来返回给外部
    //需要对它进行 计算

    private Rect rPos = new Rect(0, 0, 100, 100);

    /// <summary>
    /// 屏幕九宫格对齐方式
    /// </summary>
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    /// <summary>
    /// 控件中心对齐方式
    /// </summary>
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    /// <summary>
    /// 偏移位置
    /// </summary>
    public Vector2 pos;
    //宽高
    public float width = 100;
    public float height = 50;
    //用于计算的中心点 
    private Vector2 centerPos;

    public Rect Pos
    {
        get
        {
            //进行计算
            //计算中心点偏移
            CalcCenterPos();
            //计算屏幕坐标点
            CalcPos();
            //宽高直接赋值 返回给外部
            rPos.width = width;
            rPos.height = height;
            return rPos;
        }

    }
    /// <summary>
    /// 计算中心点偏移的方法
    /// </summary>
    public void CalcCenterPos()
    {
        switch (control_Center_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Center:
                centerPos.x = -width / 2;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
        }
    }

    public void CalcPos()
    {
        switch (screen_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Down:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Alignment_Type.Left:
                rPos.x = 0 + centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Center:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Up:
                rPos.x = centerPos.x + pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Down:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Alignment_Type.Right_Up:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right_Down:
                rPos.x = Screen.width + centerPos.x - pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
        }
    }
}
