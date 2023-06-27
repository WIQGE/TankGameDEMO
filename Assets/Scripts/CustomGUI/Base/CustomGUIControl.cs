using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Style_OnOff
{
    ON,
    OFF,
}

public abstract class CustomGUIControl : MonoBehaviour
{
    //提取控件的共同表现
    //位置信息
    public CustomGUIPos guiPos;

    //显示内容
    public GUIContent content;

    //自定义样式
    public GUIStyle style;
    //自定义样式开关
    public E_Style_OnOff styleOnOrOff = E_Style_OnOff.OFF;

    public void DrawGUI()
    {
        switch (styleOnOrOff)
        {
            case E_Style_OnOff.ON:
                StyleOnDraw();
                break;
            case E_Style_OnOff.OFF:
                StyleOffDraw();
                break;
        }
    }
    /// <summary>
    /// 不同控件子类重写方法
    /// </summary>
    protected abstract void StyleOnDraw();
    //{
    //    //GUI.Button(guiPos.Pos, content, style);
    //}
    protected abstract void StyleOffDraw();
    //{
    //    //GUI.Button(guiPos.Pos, content);
    //}
}
