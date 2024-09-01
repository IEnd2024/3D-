using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel:BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnExit;
    public Button btnAbout;
    public Button btnIntroduce;
    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            ;
        } );

        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btnExit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        btnAbout.onClick.AddListener(() =>
        {
            ;
        });

        btnIntroduce.onClick.AddListener(() =>
        {
            ;
        });
    }

}
