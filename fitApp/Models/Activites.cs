using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace fitApp.Models
{
    public class Activites
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public  string Name { get; set; }

        public  int Calories { get; set; }

        public  int Kilometers { get; set; }

        public  int Minutes { get; set; }

       
    }
}
