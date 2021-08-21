using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyPlayerList : MonoBehaviour
{
    public float DPS { get; private set; }


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
        ///넣어두자
        if (PartyPlayerList.gameObject.activeSelf)
        {
        }
        else
        {
            this.gameObject.SetActive(false);
            //스왑하는부분 넣기
        }

        PartyPanel.Instance.PartyUIUpdate();
    }
}
