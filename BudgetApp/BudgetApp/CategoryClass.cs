using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BudgetApp
{
    public class CategoryClass
    {
        [PrimaryKey, AutoIncrement]
        public int cateID { get; set; }
        public string categoryType { get; set; }
        public string categoryName { get; set; }
        public string categoryImg { get; set; }
        public string userID { get; set; }
    }
}
