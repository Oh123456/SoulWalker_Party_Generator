using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataBaseManager : MonoSingleton<DataBaseManager>
{

    public struct Data
    {
        public int id;
        public string name;
        public float dps;
        public int job;
        public int isWeapon;

        public Data(int id, string name, float dps, int job, int isWeapon)
        {
            this.id         = id;
            this.name       = name;
            this.dps        = dps;
            this.job        = job;
            this.isWeapon   = isWeapon;
        }
    }

    private DataBase dataBase;

    /// <summary>
    /// 데이터 테이블 이름
    /// </summary>
    public const string tableName = "PlayerData";
    /// <summary>
    /// 이름
    /// </summary>
    public const string colName = "Name";
    /// <summary>
    /// DPS
    /// </summary>
    public const string colDPS = "DPS";
    /// <summary>
    /// 직업
    /// </summary>
    public const string colJob = "Job";
    /// <summary>
    /// 무기유무
    /// </summary>
    public const string colIsWeapone = "IsWeapon";

    private string[] cols = new string[4] { colName, colDPS, colJob, colIsWeapone };

    public List<Data> datas = new List<Data>();

    protected override void Awake()
    {
        base.Awake();
        string filePath = Path.Combine(Application.streamingAssetsPath, "playerdata.db");
        Debug.Log(filePath);
        dataBase = new DataBase("data source = " + filePath);

        dataBase.CreateTableEx(tableName, cols , new string[] { "TEXT", "REAL", "INTEGER" , "INTEGER" });
    }

    /// <summary>
    ///  데이터 삽입
    /// </summary>
    /// <param name="values">이름 DPS 직업 무기유무 순서</param>
    public void InsertInto(string[] values)
    {
        dataBase.InsertInto(tableName, values);
    }

    /// <summary>
    /// 데이터 변경
    /// </summary>
    /// <param name="cols"> 병경할 열</param>
    /// <param name="colsvalues"> 열의 변경될 값</param>
    /// <param name="selectkey"> 조건의 키 (rowid, Name, DPS .... )</param>
    /// <param name="selectvalue">맞으면 실생할값 rowid = 1 1행의 값을 colsvalues로 바꾼다</param>
    public void UpdateInto(string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        dataBase.UpdateInto(tableName, cols, colsvalues, selectkey , selectvalue);
    }

    /// <summary>
    /// 데이터 삭제
    /// </summary>
    /// <param name="selectkey"></param>
    /// <param name="condition"></param>
    /// <param name="conditionvalue"></param>
    public void Delete(string selectkey, string condition, string conditionvalue)
    {
        dataBase.Delete(tableName, selectkey, condition, conditionvalue);
    }

    /// <summary>
    /// id값으로 데이터 삭제
    /// </summary>
    /// <param name="id"></param>
    public void DeleteToID(string id)
    {
        dataBase.Delete(tableName, "rowid", "=" , id);
    }

    /// <summary>
    /// 데이터 읽기 
    /// </summary>
    public void ReadFullTable()
    {
        SqliteDataReader reader = dataBase.ReadFullTable(tableName);
        // 데이터 추가할것
        int indx = 0;
        while (reader.Read())
            datas.Add(new Data(indx++,reader.GetString(0),reader.GetFloat(1),reader.GetInt32(2), reader.GetInt32(3)));
        
    }
}
