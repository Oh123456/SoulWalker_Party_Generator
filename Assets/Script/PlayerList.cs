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
    Image icon;
    [SerializeField]
    Image bgImage;
    [SerializeField]
    Sprite[] characterImages;

    public int Job { get; private set; }
    public int ID { get; set; }
    public float DPS { get; private set; }

    public PlayerDataScroll parentsplayerDataScroll { get; set; }

    public void Setting(DataBaseManager.Data data, PlayerDataScroll playerDataScroll)
    {
        nameFied.text = data.name;
        dpsFied.text = data.dps.ToString("F1");
        DPS = data.dps;
        Job = data.job;
        ID = data.id;
        //if (data.isWeapon == 1)
        //    toggle.isOn = true;
        //else
        //    toggle.isOn = false;

        parentsplayerDataScroll = playerDataScroll;

        bgImage.color = CharacterData.GetCharacterColor(Job);
        icon.sprite = characterImages[Job - 1];
    }


    public void OnClick()
    {
        MainPanel.Instance.SelectPlayer(this);
    }

    public void OnClickDelete()
    {
        DeletePanel.Instance.DeleteItem(this);
    }

    public void NameChange()
    {
        string userName = "'" + nameFied.text + "'";

        DataBaseManager.Instance.UpdateInto(new string[] { DataBaseManager.colName }, new string[] { userName }, "rowid" , ID.ToString());
    }

    public void DPSChange()
    {
        string userDPS = dpsFied.text;

        DataBaseManager.Instance.UpdateInto(new string[] { DataBaseManager.colDPS }, new string[] { userDPS }, "rowid", ID.ToString());
    }

}
