using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneApp.ViewModels
{
    public class Counter
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Count { get; set; }
    }
}
