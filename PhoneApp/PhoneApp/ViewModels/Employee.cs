using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneApp.ViewModels
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
