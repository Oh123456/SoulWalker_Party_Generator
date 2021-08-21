using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyList : MonoBehaviour
{
    [SerializeField]
    PartyPlayerList[] partyPlayerLists;
    [SerializeField]
    Text dpstext;

    public void Show()
    {
        // 일단 다끈다
        foreach (var item in partyPlayerLists)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void UpdateUI()
    {
        float sum = 0.0f;
        int count = 0;
        foreach (var item in partyPlayerLists)
        {
            if (item.gameObject.activeSelf == true)
            {
                sum += item.DPS;
                count++;
            }
        }

        dpstext.text = (sum / (float)count).ToString("F1");
    }
}
