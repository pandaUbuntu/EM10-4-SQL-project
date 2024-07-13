using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;
using WindowsFormsApp6.Entity;
using WindowsFormsApp6.Services;
using WindowsFormsApp6.Services.DataBase;

namespace WindowsFormsApp6.Repository
{
    public class CityRepository
    {
        private readonly string _tableName = "city";
        private readonly QueryBuilder _queryBuilder;
        private string[] props = new string[] { "name", "district", "countryCode", "population" };

        public CityRepository()
        {
            _queryBuilder = new QueryBuilder(Singleton.GetInstance().getConnection());
        }

        public City getOne(int id)
        {
            MySqlDataReader myReader = null;
            try
            {
               myReader = _queryBuilder
                    .select(_tableName)
                    .where<int>("id", id)
                    .getQueryResult();

                myReader.Read();
                    City city = new City { 
                        Id = Convert.ToInt32(myReader["id"]), 
                        Name = myReader["name"].ToString(), 
                        District = myReader["district"].ToString(),
                        CountryCode = myReader["CountryCode"].ToString(), 
                        Population = Convert.ToInt32(myReader["Population"]) };

                    return city;
            }
            finally
            {
                myReader.Close();
                Singleton.GetInstance().getConnection().Close();
            }
        }

        public int create(string name = "", string district = "", string countryCode = "", int population = 0)
        {
            int id = -1;
            try
            {
                
                string[] values = new string[] { name, district, countryCode, population.ToString() };

                id = _queryBuilder.insert(_tableName, props, values).getNonQueryResult();

               
                return id;
            }
            finally
            {
                Singleton.GetInstance().getConnection().Close();
            }
        }

        public void update<T>(string whereField, T param, string[] propNames, string[] data)
        {
            try
            {
                _queryBuilder
                    .update(_tableName, propNames, data)
                    .where(whereField, param)
                    .getNonQueryResult();
            }
            finally
            {
                Singleton.GetInstance().getConnection().Close();
            }
        }

        public List<City> getAll(int start = 0, int limit = 25, QueryBuilder.OrderType order = QueryBuilder.OrderType.NONE)
        {
            List<City> list = new List<City>(); 

            MySqlDataReader myReader = _queryBuilder
                                        .select("city")
                                        .order("name", order)
                                        .offset(start, limit)
                                        .getQueryResult();
            try
            {
                while (myReader.Read())
                {

                    City city = new City
                    {
                        Id = Convert.ToInt32(myReader["id"]),
                        Name = myReader["name"].ToString(),
                        District = myReader["district"].ToString(),
                        CountryCode = myReader["CountryCode"].ToString(),
                        Population = Convert.ToInt32(myReader["Population"])
                    };

                    list.Add(city);
                }

                return list;
            }
            finally
            {
                myReader.Close();
                Singleton.GetInstance().getConnection().Close();
            }
        }
    }
}
