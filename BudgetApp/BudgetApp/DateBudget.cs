using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp
{
    class DateBudget
    {
        public string Date { get; set; }
        public int Expense { get; set; }
        public int Income { get; set; }
        public DateBudget(string Date)
        {
            this.Date = Date;
        }
    }
}
