using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyPlayerList : MonoBehaviour
{
    [SerializeField]
    Text username;
    [SerializeField]
    Text dpstext;
    [SerializeField]
    Image icon;
    [SerializeField]
    Image bg;
    [SerializeField]
    Sprite[] characterImages;
    public string UserName { get { return username.text; } }
    public float DPS { get; private set; }
    public int Job { get; private set; }
    [HideInInspector]
    public bool isOn = false;

    public void Setting(PlayerList playerList)
    {
        username.text = playerList.userName;
        DPS = playerList.DPS;
        dpstext.text = DPS.ToString("F1");
        Job = playerList.Job;
        icon.sprite = characterImages[Job - 1];
        bg.color = CharacterData.GetCharacterColor(Job);
        isOn = true;
    }

    public void Setting(string userName, float dps , int job)
    {
        username.text = userName;
        DPS = dps;
        dpstext.text = DPS.ToString("F1");
        Job = job;
        icon.sprite = characterImages[Job - 1];
        bg.color = CharacterData.GetCharacterColor(Job);
        isOn = true;
    }

    public void ZeroSetting()
    {
        username.text = "";
        DPS = 0.0f;
        dpstext.text = DPS.ToString("F1");
        Job = 0;
        icon.sprite = null;
        bg.color = Color.black;
        isOn = false;
    }


    public void OnClicked()
    {
        if (PartyPanel.Instance.selectedPartyPlayerList == null)
            PartyPanel.Instance.selectedPartyPlayerList = this;
        else
        {
            this.SwapData(PartyPanel.Instance.selectedPartyPlayerList);
            PartyPanel.Instance.selectedPartyPlayerList = null;
        }
    }

    public void SwapData(PartyPlayerList PartyPlayerList)
    {
        if (isOn == false && PartyPlayerList.isOn == false)
            return;
        if (isOn)
        {
            if (PartyPlayerList.isOn)
            {
                string newusername = UserName;
                float newpds = DPS;
                int newjob = Job;

                this.Setting(PartyPlayerList.UserName, PartyPlayerList.DPS, PartyPlayerList.Job);
                PartyPlayerList.Setting(newusername, newpds, newjob);
            }
            else
            {
                string newusername = UserName;
                float newpds = DPS;
                int newjob = Job;

                PartyPlayerList.Show();
                PartyPlayerList.Setting(newusername, newpds, newjob);


                Hide();
                isOn = false;
            }
        }
        else
        {
            string newusername = PartyPlayerList.UserName;
            float newpds = PartyPlayerList.DPS;
            int newjob = PartyPlayerList.Job;

            Show();
            Setting(newusername, newpds, newjob);


            PartyPlayerList.Hide();
            PartyPlayerList.isOn = false;

        }
        PartyPanel.Instance.PartyUIUpdate();
    }

    public void Hide()
    {
        username.gameObject.SetActive(false);
        dpstext.gameObject.SetActive(false) ;
        //icon   .sprite = null;
        //bg.color = Color.black;
        ZeroSetting();
    }

    public void Show()
    {
        username.gameObject.SetActive   (true);
        dpstext.gameObject.SetActive    (true);
        if (Job > 0)
            icon.sprite = characterImages[Job - 1];
        bg.color = CharacterData.GetCharacterColor(Job);
    }
}
