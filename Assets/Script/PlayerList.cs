using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : MonoBehaviour
{
    [SerializeField]
    InputField nameFied;
    [SerializeField]
    InputField dpsFied;
    [SerializeField]
    Toggle toggle;
    [SerializeField]
    Button button;

    public int Job { get; private set; }
    public int ID { get; private set; }
    public float DPS { get; private set; }

    public PlayerDataScroll parentsplayerDataScroll { get; set; }

    public void Setting(DataBaseManager.Data data, PlayerDataScroll playerDataScroll)
    {
        nameFied.text = data.name;
        dpsFied.text = data.dps.ToString("F1");
        DPS = data.dps;
        Job = data.job;
        ID = data.id;
        if (data.isWeapon == 1)
            toggle.isOn = true;
        else
            toggle.isOn = false;

        parentsplayerDataScroll = playerDataScroll;
    }


    public void OnClick()
    {
        MainPanel.Instance.SelectPlayer(this);
    }
}
