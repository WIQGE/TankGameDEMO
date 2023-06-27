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
    //��ȡ�ؼ��Ĺ�ͬ����
    //λ����Ϣ
    public CustomGUIPos guiPos;

    //��ʾ����
    public GUIContent content;

    //�Զ�����ʽ
    public GUIStyle style;
    //�Զ�����ʽ����
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
    /// ��ͬ�ؼ�������д����
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
