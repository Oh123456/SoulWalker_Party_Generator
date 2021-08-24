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

    public PlayerDataScroll PlayerDataScroll
    { get { return playerDataScroll; } }


    private void Start()
    {
        UpdateUI();
    }

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
    public void UpdateUI()
    {
        int count = selectedPlayerdataScroll.PlayerLists.Count;
        playerCount.text = "총 인원 : " + count.ToString();

        partyCoung.text = "파티 수 : " + ((int)(count / 4)).ToString();

        float sum = 0.0f;
        foreach (var item in selectedPlayerdataScroll.PlayerLists)
        {
            sum += item.DPS;
        }

        if (count != 0)
            avgDPS.text = "평균 DPS : " + (sum / count).ToString("F1");
        else
            avgDPS.text = "평균 DPS : 0.0 ";
    }


    public void OnAdd()
    {
        PlayerAddPanel.Instance.gameObject.SetActive(true);
    }

    public void OnDelete()
    {
        DeletePanel.Instance.gameObject.SetActive(true);
    }

    public void GoToParty()
    {
        if (selectedPlayerdataScroll.PlayerLists.Count >= 4)
            PartyPanel.Instance.Show(selectedPlayerdataScroll.PlayerLists);

    }

    public void ListUpdate()
    {
        playerDataScroll.DataUpdate();
        selectedPlayerdataScroll.Clear();
    }

    public void ListAllUpdate()
    {
        playerDataScroll.UpdateScrollList();
        selectedPlayerdataScroll.UpdateScrollList();
    }
}
