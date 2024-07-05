using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp6.Services.DataBase;

namespace WindowsFormsApp6.Services
{
    public sealed class Singleton
    {
        private MySqlConnection _connection;
        private Singleton() { }

        private static Singleton _instance;

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        public void setConnection(MySqlConnection connection)
        {
            this._connection = connection;
        }

        public MySqlConnection getConnection(string dbName = "world") 
        { 
            if (this._connection == null)
            {
                this._connection = DataBaseConnector.getConnection(dbName: dbName);
            }

            return this._connection;
        }
    }
}
