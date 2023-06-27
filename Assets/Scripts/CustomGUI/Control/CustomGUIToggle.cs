using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public bool isSel;

    public event UnityAction<bool> changeValue;

    private bool isOldSel;
    protected override void StyleOffDraw()
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content);
        //当选项变化时 才执行函数 这样降低性能消耗
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }

    protected override void StyleOnDraw()
    {
        isSel = GUI.Toggle(guiPos.Pos, isSel, content,style);
        if (isOldSel != isSel)
        {
            changeValue?.Invoke(isSel);
            isOldSel = isSel;
        }
    }
}
