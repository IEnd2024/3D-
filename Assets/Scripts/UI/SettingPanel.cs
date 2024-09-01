using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Dropdown drMusic;
    public Slider slMusic;
    public Dropdown drSound;
    public Slider slSound;
    public InputField inputCheat;
    public Button btnBack;

    public override void Init()
    {
        drMusic.onValueChanged.AddListener((v) =>
        {
            ;
        } );

        slMusic.onValueChanged.AddListener((v) =>
        {
            ;
        });

        drSound.onValueChanged.AddListener((v) =>
        {
            ;
        });

        slSound.onValueChanged.AddListener((v) =>
        {
            ;
        });

        inputCheat.onValueChanged.AddListener((v) =>
        {
            ;
        });
        btnBack.onClick.AddListener(() =>
        {
            if (UIManager.Instance.IsGameStart==false)
            {
                UIManager.Instance.HidePanel<SettingPanel>();
                UIManager.Instance.ShowPanel<SettingPanel>();
            }
        });
    }
}
