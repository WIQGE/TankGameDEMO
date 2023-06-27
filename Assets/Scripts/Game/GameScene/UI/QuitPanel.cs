using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public CustomGUIButton btnClose;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnGoOn;
    // Start is called before the first frame update
    void Start()
    {
        btnClose.clickEvent += () =>
        {
            GamePanel.Instance.ShowMe();
            HideMe();
        };
        btnQuit.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };
        btnGoOn.clickEvent += () =>
        {
            GamePanel.Instance.ShowMe();
            HideMe();
        };

        HideMe();
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }

}
