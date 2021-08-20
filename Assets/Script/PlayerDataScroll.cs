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

    private void OnEnable()
    {
        if (isPlayerList)
            DataUpdate();
    }

    private void Start()
    {
        if (isPlayerList)
        {
            DataUpdate();
            //DataBaseManager.Instance.ReadFullTable();


            //int count = DataBaseManager.Instance.datas.Count;

            //float height = playerList.GetComponent<RectTransform>().rect.height;
            //float width = content.rect.width;

            //content.sizeDelta = new Vector2(0.0f, height * count);

            //playerLists?.Clear();

            //for (int i = 0; i < count; i++)
            //{
            //    PlayerList spawnObject = Instantiate<PlayerList>(playerList, content.transform);
            //    spawnObject.Setting(DataBaseManager.Instance.datas[i], this);
            //    playerLists.Add(spawnObject);
            //}
        }
        else
        {
            content.sizeDelta = new Vector2(0.0f, 0.0f);
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
        foreach (var item in playerLists)
        {
            if (item == null)
                count--;
        }

        float height = playerList.GetComponent<RectTransform>().rect.height;
        float width = content.rect.width;

        content.sizeDelta = new Vector2(width, height * count);
    }


    public void AddData(string name, float dps, int job , int isWeapon = 1)
    {
        DataBaseManager.Data newdata = new DataBaseManager.Data(DataBaseManager.Instance.lastID,name, dps, job , isWeapon);
        DataBaseManager.Instance.datas.Add(newdata);
        DataBaseManager.Instance.dataCount++;

        PlayerList spawnObject = Instantiate<PlayerList>(playerList, content);
        spawnObject.Setting(newdata, this);
        playerLists.Add(spawnObject);
    }

    public void DataUpdate()
    {
        Clear();

        DataBaseManager.Instance.ReadFullTable();


        int count = DataBaseManager.Instance.datas.Count;

        float height = playerList.GetComponent<RectTransform>().rect.height;
        float width = content.rect.width;

        content.sizeDelta = new Vector2(0.0f, height * count);

        playerLists?.Clear();

        for (int i = 0; i < count; i++)
        {
            PlayerList spawnObject = Instantiate<PlayerList>(playerList, content.transform);
            spawnObject.Setting(DataBaseManager.Instance.datas[i], this);
            playerLists.Add(spawnObject);
        }
    }

    /// <summary>
    /// 리스트 클리어
    /// </summary>
    public void Clear()
    {
        //오브젝트 풀링 만들기 귀찮다 리스트 얼마없을거 같으니 그냥한다
        int childCount = content.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        playerLists?.Clear();
    }

    public void DeleteItem(PlayerList playerList)
    {
        foreach (var item in playerLists.ToList())
        {
            
            if (playerList.ID == item.ID)
            {
                DataBaseManager.Instance.DeleteToID(item.ID.ToString());
                playerLists.Remove(item);
                Destroy(item.gameObject);
                break;
            }
            
        }

        UpdateScrollList();
    }
}
