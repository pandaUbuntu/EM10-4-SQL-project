using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp6.Entity
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string CountryCode { get; set; }
        public int Population { get; set; }
            
        public City() { 
        }
    }
}
