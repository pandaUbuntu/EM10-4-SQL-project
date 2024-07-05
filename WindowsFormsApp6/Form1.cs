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


            List<City> list = cityRepository.getAll(25, 50);

            return;
        }
    }
}
