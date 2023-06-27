using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    private CustomGUIControl[] allControls;
    private void OnGUI()
    {
        //每一次绘制前 得到所有子对象控件的 父类脚本
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
