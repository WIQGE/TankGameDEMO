using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //记录上一次为true的toggle
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
                //当传入的value 是 true时 把另外两个变为false
                if (value == true)
                {
                    //意味着另外两个变成false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    //记录上一次为True的Toggle
                    frontTurTog = toggle;
                }
                //来判断当前编程false的toggle是不是上次为true的
                else if(toggle == frontTurTog)
                {
                    //强制改成true
                    toggle.isSel = true;
                }
            };
        }
    }


}
