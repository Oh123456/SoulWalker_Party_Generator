using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyPanel : MonoSingleton<PartyPanel>
{
    [SerializeField]
    PartyList[] partyLists;


    public PartyPlayerList selectedPartyPlayerList;


    public void PartyUIUpdate()
    {
        foreach (var item in partyLists)
            item.UpdateUI();
    }
}
