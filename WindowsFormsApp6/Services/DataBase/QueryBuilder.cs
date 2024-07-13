using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Services.DataBase
{
    public class QueryBuilder
    {
        public enum OrderType
        {
            ASC,
            DESC,
            NONE
        }

        private MySqlConnection mySqlConnection = null;
        private string query = "";

        public QueryBuilder(MySqlConnection mySqlConnection)
        {
            this.mySqlConnection = mySqlConnection;
        }

        private void setTable(string tableName)
        {
            query += " FROM  " + tableName + " ";
        }

        private string prepareString(string text)
        {
            return "\'" + text + "\'";
        }

        private string prepareUpdateString(string[] propNames, string[] data)
        {
            string[] updateStr = new string[propNames.Length];

            for (int i = 0; i < propNames.Length; i++)
            {
                updateStr[i] = $" {propNames[i]} = {prepareString(data[i])}";
            }

            return string.Join(", ", updateStr);
        }

        public QueryBuilder select(string tableName, string[] props = null)
        {
            mySqlConnection.Open();

            if (props != null)
            {

            }
            else
            {
                query += "SELECT *";
            }

            this.setTable(tableName);

            return this;
        }
        public QueryBuilder insert(string tableName, string[] propNames, string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = prepareString(data[i]);
            }

            mySqlConnection.Open();

            query += $"INSERT INTO {tableName} ({string.Join(", ", propNames)}) VALUES ({string.Join(", ", data)})";

            return this;
        }
        public QueryBuilder update(string tableName, string[] propNames, string[] data)
        {
            mySqlConnection.Open();

            query += $"UPDATE {tableName} SET {prepareUpdateString(propNames, data)}";

            return this;
        }
        public QueryBuilder delete(string tableName)
        {
            mySqlConnection.Open();

            query += "DELETE ";

            this.setTable(tableName);
            return this;
        }

        public QueryBuilder where<T>(string field, T value)
        {
            query += $" WHERE {field} = {value}";

            return this;
        }

        public QueryBuilder order (string field, OrderType orderType = OrderType.ASC) 
        {
            if (orderType != OrderType.NONE)
            {
                query += $" ORDER BY {field} {orderType.ToString()}";
            }
            

            return this;
        }

        public QueryBuilder offset(int offset, int count)
        {

            query += $" LIMIT {count} OFFSET {offset} ";

            return this;
        }

        public string getQuery()
        {
            try
            {
                return query;
            }
            finally 
            {
                query = "";
            }
        }

        public MySqlDataReader getQueryResult()
        {
            try
            {
                return (new MySqlCommand(query, mySqlConnection)).ExecuteReader();
            }
            finally
            {
                query = "";
            }
        }

        public int getNonQueryResult()
        {
            try
            {
                return (new MySqlCommand(query, mySqlConnection)).ExecuteNonQuery();
            }
            finally
            {
                query = "";
            }
        }
    }
}
