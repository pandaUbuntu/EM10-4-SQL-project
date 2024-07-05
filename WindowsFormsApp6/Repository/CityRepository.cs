using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WindowsFormsApp6.Entity;
using WindowsFormsApp6.Services;

namespace WindowsFormsApp6.Repository
{
    public class CityRepository
    {
        public City getOne(int id)
        {
            Singleton.GetInstance().getConnection().Open();

            string sql = "" +
                "SELECT * " +
                "FROM city " +
                "WHERE id = " + id;

            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();
            try
            {
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

        public List<City> getAll(int start = 0, int limit = 25)
        {
            List<City> list = new List<City>(); 
            Singleton.GetInstance().getConnection().Open();

            string sql = "" +
                "SELECT * " +
                "FROM city " +
                $"LIMIT {start},{limit}";

            MySqlCommand command = new MySqlCommand(sql, Singleton.GetInstance().getConnection());

            MySqlDataReader myReader = command.ExecuteReader();
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
