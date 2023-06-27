using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    //�����ؼ�
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnSure;

    private void Start()
    {
        btnSure.clickEvent += () =>
        {
            Time.timeScale = 1;

            //�����ݼ�¼�����а���
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text, 
                GamePanel.Instance.nowScore, 
                GamePanel.Instance.nowTime);
            //�ص���ʼ������
            SceneManager.LoadScene("BeginScene");
        };

        HideMe();
    }

}
