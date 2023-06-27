using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIInput : CustomGUIControl
{
    public event UnityAction<string> textChange;

    private string oldStr = "";
    protected override void StyleOffDraw()
    {
        content.text = GUI.TextField(guiPos.Pos,content.text);
        if(content.text != oldStr )
        {
            textChange?.Invoke(content.text);
            oldStr = content.text;
        }
    }

    protected override void StyleOnDraw()
    {
        content.text = GUI.TextField(guiPos.Pos, content.text, style);
        if (content.text != oldStr)
        {
            textChange?.Invoke(content.text);
            oldStr = content.text;
        }
    }
}
