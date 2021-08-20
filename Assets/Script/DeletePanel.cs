using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePanel : MonoSingleton<DeletePanel>
{
    [SerializeField]
    PlayerDataScroll playerDataScroll;

    public void OnBack()
    {
        gameObject.SetActive(false);
        MainPanel.Instance.ListUpdate();
        MainPanel.Instance.UpdateUI();
    }

    public void DeleteItem(PlayerList playerList)
    {
        playerDataScroll.DeleteItem(playerList);
    }
}
