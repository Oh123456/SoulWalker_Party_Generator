using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoSingleton<MainPanel>
{
    [SerializeField]
    PlayerDataScroll playerDataScroll;
    [SerializeField]
    PlayerDataScroll selectedPlayerdataScroll;
    [SerializeField]
    Text playerCount;
    [SerializeField]
    Text partyCoung;
    [SerializeField]
    Text avgDPS;

    public void SelectPlayer(PlayerList SelectedPlayerList)
    {
        if (SelectedPlayerList.parentsplayerDataScroll == playerDataScroll)
        {
            playerDataScroll.SendPlayerList(SelectedPlayerList, selectedPlayerdataScroll);
        }
        else
        {
            selectedPlayerdataScroll.SendPlayerList(SelectedPlayerList, playerDataScroll);
        }
        UpdateUI();
    }


    /// <summary>
    /// UI 업데이트
    /// </summary>
    void UpdateUI()
    {
        int count = selectedPlayerdataScroll.PlayerLists.Count;
        playerCount.text = "총 인원 : " + count.ToString();

        partyCoung.text = "파티 수 : " + ((int)(count / 4)).ToString();

        float sum = 0.0f;
        foreach (var item in selectedPlayerdataScroll.PlayerLists)
        {
            sum += item.DPS;
        }

        avgDPS.text = "평균 DPS : " + (sum / count).ToString("F1");
    }

}
