using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    private CustomGUIControl[] allControls;
    private void OnGUI()
    {
        //ÿһ�λ���ǰ �õ������Ӷ���ؼ��� ����ű�
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
