using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //��¼��һ��Ϊtrue��toggle
    private CustomGUIToggle frontTurTog;
    void Start()
    {
        if (toggles == null)
        {
            return;
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValue += (value) =>
            {
                //�������value �� trueʱ ������������Ϊfalse
                if (value == true)
                {
                    //��ζ�������������false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    //��¼��һ��ΪTrue��Toggle
                    frontTurTog = toggle;
                }
                //���жϵ�ǰ���false��toggle�ǲ����ϴ�Ϊtrue��
                else if(toggle == frontTurTog)
                {
                    //ǿ�Ƹĳ�true
                    toggle.isSel = true;
                }
            };
        }
    }


}
