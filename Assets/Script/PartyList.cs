using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PartyList : MonoBehaviour
{
    [SerializeField]
    PartyPlayerList[] partyPlayerLists;
    [SerializeField]
    Text dpstext;
    private List<PlayerList> playerLists;

    public void Setting(List<PlayerList> playerLists)
    {
        this.playerLists = playerLists.ToList();

        int count = playerLists.Count;
        for (int i = 0; i < 4; i++)
        {
            if (i < count)
            {
                partyPlayerLists[i].Show();
                partyPlayerLists[i].Setting(playerLists[i]);
            }
            else
            {
                partyPlayerLists[i].Hide();
            }

        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        float sum = 0.0f;
        int count = 0;
        foreach (var item in partyPlayerLists)
        {
            if (item.isOn)
            {
                sum += item.DPS;
                count++;
            }
        }

        dpstext.text = (sum / (float)count).ToString("F1");
    }

    public void Clear()
    {
        playerLists?.Clear();
        foreach (var item in partyPlayerLists)
        {
            item.Hide();
        }

    }
}
