using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private SqliteConnection sqliteConnection;
    private SqliteCommand sqliteCommand;
    private SqliteDataReader reader;


    public DataBase(string connectionString)
    {
        OpenDatabase(connectionString);
    }


    public void OpenDatabase(string connectionString)
    {
        sqliteConnection = new SqliteConnection(connectionString);
        sqliteConnection.Open();
        Debug.Log("Connected to database");
    }

    public void CloseSqlConnection()
    {
        if (sqliteCommand != null)
        {
            sqliteCommand.Dispose();
        }

        sqliteCommand = null;

        if (reader != null)
        {
            reader.Dispose();
        }

        reader = null;

        if (sqliteConnection != null)
        {
            sqliteConnection.Close();
        }

        sqliteConnection = null;
        Debug.Log("Disconnected from database.");
    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = sqlQuery;

        reader = sqliteCommand.ExecuteReader();

        return reader;
    }

    public object ExecuteScalar(string sqlQuery)
    {
        sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = sqlQuery;

        return sqliteCommand.ExecuteScalar();

    }

    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + cols[0];
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        query += ") VALUES (" + values[0];
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {

        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {

            query += ", " + cols[i] + " =" + colsvalues[i];
        }

        query += " WHERE " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }

    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        return ExecuteQuery(query);
    }

    public SqliteDataReader Delete(string tableName, string selectkey, string condition, string conditionvalue)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + selectkey + " " + condition + " " + conditionvalue;
        return ExecuteQuery(query);
    }

    public SqliteDataReader CreateTableEx(string name, string[] col, string[] colType)
    {
        string query = string.Format("SELECT COUNT(*) FROM sqlite_master WHERE Name = '{0}'", name);
        int result = Convert.ToInt32(ExecuteScalar(query));
        if (result < 1)
            return CreateTable(name, col, colType);
        return ExecuteQuery(query);
    }

    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }

        return ExecuteQuery(query);
    }
}
