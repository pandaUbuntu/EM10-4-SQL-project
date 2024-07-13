using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp6.Entity;
using WindowsFormsApp6.Repository;
using WindowsFormsApp6.Services;
using WindowsFormsApp6.Services.DataBase;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MySqlConnection connection = Singleton.GetInstance().getConnection();

            CityRepository cityRepository = new CityRepository();

            City city = cityRepository.getOne(256);

            
            cityRepository.update("id", 256, new string[] { "name" }, new string[] { "New vegas" });

            city = cityRepository.getOne(256);

            List<City> list = cityRepository.getAll(25, 50);

            //int id = cityRepository.create("Корсунь", "Корсунський", "UKR", 70000);

            return;
        }
    }
}
