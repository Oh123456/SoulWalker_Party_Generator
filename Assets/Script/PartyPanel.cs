using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PartyPanel : MonoSingleton<PartyPanel>
{
    [SerializeField]
    PartyList[] partyLists;

    [HideInInspector]
    public PartyPlayerList selectedPartyPlayerList;


    public void Show(List<PlayerList> playerLists)
    {
        gameObject.SetActive(true);
        int partyCount = Mathf.CeilToInt(((float)playerLists.Count / 4.0f));
        int tempCount = playerLists.Count / 4;
        var orderbyList = playerLists.OrderBy(x => x.DPS).ToList();
        LinkedList<PlayerList> players = new LinkedList<PlayerList>();

        foreach (var item in orderbyList) 
        {
            players.AddLast(item);
        }


        List<List<PlayerList>> partyPlayers = new List<List<PlayerList>>();
        for (int i = 0; i < partyCount; i++)
            partyPlayers.Add(new List<PlayerList>());
        for (int j = 0; j < 2; j++)
        {
            // 임시
            for (int i = 0; i </* partyCount*/tempCount; i++)
            {
                if (players.Count > 0)
                {
                    partyPlayers[i].Add(players?.Last());
                    players?.RemoveLast();
                }
                if (players.Count > 0)
                {
                    partyPlayers[i].Add(players?.First());
                    players?.RemoveFirst();
                }
            }
        }

        // 임시
        // 남아있다면
        if (players.Count != 0)
            partyPlayers[partyCount - 1] = players.ToList();

        int count = 0;
        foreach (var item in partyLists)
        {
            if (partyCount > count)
            {
                item.gameObject.SetActive(true);
                item.Setting(partyPlayers[count]);
            }
            else
                item.gameObject.SetActive(false);
            count++;
        }
    }

    public void PartyUIUpdate()
    {
        foreach (var item in partyLists)
            item.UpdateUI();
    }

    public void OnBack()
    {
        gameObject.SetActive(false);
        selectedPartyPlayerList = null;
    }
}
