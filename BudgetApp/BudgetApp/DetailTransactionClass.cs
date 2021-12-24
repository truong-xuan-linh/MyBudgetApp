using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BudgetApp
{
    public class DetailTransactionClass
    {
        [PrimaryKey, AutoIncrement]
        public int bID { get; set; }
        public string icon { get; set; }
        public string transactionName { get; set; }
        public string transactionColor { get; set; }
        public int transactionMoney { get; set; }
        public string transactionDay { get; set; }
        public string categoryType { get; set; }
    }
}
