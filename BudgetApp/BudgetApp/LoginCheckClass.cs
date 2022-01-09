using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp
{
    public class LoginCheckClass
    {
        [PrimaryKey, AutoIncrement]
        public int cID { get; set; }
        public string userID { get; set; }
        public bool isLogin { get; set; }
        public string userName { get; set; }
    }
}
