using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp
{
    public class TransactionDateClass: List<DetailTransactionClass>
    {
        public string date { get; set; }
        public int expense { get; set; }
        public int income { get; set; }
    }
}
