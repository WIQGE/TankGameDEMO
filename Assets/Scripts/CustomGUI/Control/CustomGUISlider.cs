using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_SliderType
{
    Horizontal,
    Vertical,
}

public class CustomGUISlider : CustomGUIControl
{
    //��ǰֻ
    public float nowValue = 0;
    //��С���ֵ
    public float minValue = 0;
    public float maxValue = 1;
    //ˮƽ������ֱ
    public E_SliderType type = E_SliderType.Horizontal;
    //�϶���ťstyle
    private GUIStyle styleThumb;

    public UnityAction<float> changeVale;
    private float oldValue;

    protected override void StyleOffDraw()
    {
        switch (type)
        {
            case E_SliderType.Horizontal:
                nowValue = GUI.HorizontalSlider(guiPos.Pos, nowValue, minValue, maxValue);
                break;
            case E_SliderType.Vertical:
                nowValue = GUI.VerticalSlider(guiPos.Pos, nowValue, minValue, maxValue);
                break;
        }
        if(oldValue != nowValue)
        {
            changeVale?.Invoke(nowValue);
            oldValue = nowValue;
        }
    }

    protected override void StyleOnDraw()
    {
        switch (type)
        {
            case E_SliderType.Horizontal:
                nowValue = GUI.HorizontalSlider(guiPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                break;
            case E_SliderType.Vertical:
                nowValue = GUI.VerticalSlider(guiPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                break;
        }
        if (oldValue != nowValue)
        {
            changeVale?.Invoke(nowValue);
            oldValue = nowValue;
        }
    }
}
