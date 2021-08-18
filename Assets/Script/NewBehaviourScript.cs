using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string filePath = Path.Combine(Application.streamingAssetsPath, "playerdata.db");
        //Debug.Log(filePath);
        //DataBase dataBase = new DataBase("data source = " + filePath);

        ////생성문
        //dataBase.CreateTableEx("PlayerData_Test2",new string[] { "Name", "DPS", "Job" } ,new string[] { "TEXT", "REAL", "INTEGER" });

        //// 데이터 삽입
        //dataBase.InsertInto("PlayerData_Test", new string[] { "'김아무개1'", "123.5","2"});

        //var asdf = dataBase.ReadFullTable("PlayerData_Test");
        //// 리드하면 다음으로 넘어감
        //asdf.Read();
        //var asdfs = asdf.GetString(0);
        //Debug.Log(asdfs);

        //asdf.Read();
        //var asdfs2 = asdf.GetString(0);
        //Debug.Log(asdfs2);

        //asdf.Read();
        //asdfs = asdf.GetString(0);
        //Debug.Log(asdfs);

        //asdf.Read();
        //asdfs = asdf.GetString(0);
        //Debug.Log(asdfs);

        ////asdf.Close();

        //// rowid = 키값
        //// 값이 텍스트일경우 '' 추가
        //// 바꿀값의 이름? = 변경값
        //// 뒤는 조건문
        //// ex 이름이 김아무개인것은 모두 바꾼다
        ////dataBase.UpdateInto("PlayerData_Test", new string[] { "Name" }, new string[] { "'몽총이'" }, "Name", "'김아무개'");
        //dataBase.Delete("PlayerData_Test", "rowid", "=" , "5");

        List<float> datas = new List<float>();
        for (int i = 0; i < 16; i++)
        {
            float ram = Random.Range(700, 1200);
            Debug.Log(i + "번째 값 : " + ram);
            datas.Add(ram);
        }


        datas.Sort();
        LinkedList<float> listDatas = new LinkedList<float>();


        float sum = 0;
        foreach (var item in datas)
        {
            sum += item;
            listDatas.AddLast(item);
        }
        Debug.Log("총합 : " + sum);

        float avg = sum / 16.0f;
        Debug.Log("평균 : " + avg);

        Dictionary<int, List<float>> paty = new Dictionary<int, List<float>>();

        //일단 4파티
        List<float>[] p = new List<float>[4];
        p[0] = new List<float>();
        p[1] = new List<float>();
        p[2] = new List<float>();
        p[3] = new List<float>();


        p[0].Add(listDatas.Last());
        listDatas.RemoveLast();
        //p[0].Add(listDatas.First());
        //listDatas.RemoveFirst();

        p[1].Add(listDatas.Last());
        listDatas.RemoveLast();
        //p[1].Add(listDatas.First());
        //listDatas.RemoveFirst();

        p[2].Add(listDatas.Last());
        listDatas.RemoveLast();
        //p[2].Add(listDatas.First());
        //listDatas.RemoveFirst();

        p[3].Add(listDatas.Last());
        listDatas.RemoveLast();
        //p[3].Add(listDatas.First());
        //listDatas.RemoveFirst();



        for (int i = 0; i < 4; i++)
        {
            p[i].Add(listDatas.Last());
            listDatas.RemoveLast();
            p[i].Add(listDatas.First());
            listDatas.RemoveFirst();

            paty.Add(i,p[i]);
        }

        paty[0].Add(listDatas.First());
        listDatas.RemoveFirst();

        paty[1].Add(listDatas.First());
        listDatas.RemoveFirst();

        paty[2].Add(listDatas.First());
        listDatas.RemoveFirst();

        paty[3].Add(listDatas.First());
        listDatas.RemoveFirst();

        float sum2 = 0;
        foreach (var item in paty)
        {
            sum2 = 0;
            foreach (var data in item.Value)
            {
                sum2 += data;
            }

            Debug.Log(item.Key + "파티 총합 :" + sum2);
            sum2 /= 4.0f;
            Debug.Log(item.Key + "파티 평균 :" + sum2);
        }

    }

    void test()
    {

    }


    
}
