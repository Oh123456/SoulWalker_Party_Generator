using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDataScroll : MonoBehaviour
{
    [SerializeField]
    RectTransform content;
    [SerializeField]
    PlayerList playerList;

    List<PlayerList> playerLists = new List<PlayerList>();
    public RectTransform ScrollContent { get { return content; } }

    /// <summary>
    /// 플레이어 리스트인지
    /// </summary>
    public bool isPlayerList = false;


    public List<PlayerList> PlayerLists
    {
        get { return playerLists; }
    }

    private void Start()
    {
        if (isPlayerList)
        {
            DataBaseManager.Instance.ReadFullTable();
            //DataBaseManager.Instance.InsertInto(new string[] {"'몽총이'","1000.0","0" ,"1" });
            //DataBaseManager.Instance.InsertInto(new string[] {"'혜나니이임'","1234.5","0" ,"1" });
            //DataBaseManager.Instance.InsertInto(new string[] {"'머장'","1111.1","0" ,"1" });
            //DataBaseManager.Instance.InsertInto(new string[] {"'김픽쉬'","1222.2","0" ,"1" });


            int count = DataBaseManager.Instance.datas.Count;

            float height = playerList.GetComponent<RectTransform>().rect.height;
            float width = content.rect.width;

            content.sizeDelta = new Vector2(width, height * count);

            playerLists?.Clear();

            for (int i = 0; i < count; i++)
            {
                PlayerList spawnObject = Instantiate<PlayerList>(playerList, content);
                spawnObject.Setting(DataBaseManager.Instance.datas[i], this);
                playerLists.Add(spawnObject);
            }
        }
        else
        {
            content.sizeDelta = new Vector2(content.rect.width, 0.0f);
        }
    }

    /// <summary>
    /// 다른 스크롤로 플레이어 리스트를 보낸다
    /// </summary>
    /// <param name="playerList"> 보내는 정보</param>
    /// <param name="destPlayerDataScroll"> 받을곳</param>
    public void SendPlayerList(PlayerList playerList, PlayerDataScroll destPlayerDataScroll)
    {
        foreach (var item in playerLists)
        {
            if (item == playerList)
            {
                item.transform.parent = destPlayerDataScroll.ScrollContent.transform;
                item.parentsplayerDataScroll = destPlayerDataScroll;
                playerLists.Remove(item);
                Debug.Log("출발지의 남은 리스트수 : " + playerLists.Count);
                destPlayerDataScroll.PlayerLists.Add(item);
                Debug.Log("목적지의 남은 리스트수 : " + destPlayerDataScroll.PlayerLists.Count);
                this.UpdateScrollList();
                destPlayerDataScroll.UpdateScrollList();
                break;
            }
        }

    }

    /// <summary>
    /// 스크롤 리스트 업데이트
    /// </summary>
    public void UpdateScrollList()
    {
        int count = playerLists.Count;

        float height = playerList.GetComponent<RectTransform>().rect.height;
        float width = content.rect.width;

        content.sizeDelta = new Vector2(width, height * count);
    }

}
