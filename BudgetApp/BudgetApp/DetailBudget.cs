using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp
{
    public class DetailBudget
    {
        public string Icon { get; set; }
        public string Category { get; set; }
        public string CategoryColor { get; set; }
        public int CategoryMoney { get; set; }
        public string CategoryDay { get; set; }
        public DetailBudget(string Category)
        {
            this.Category = Category;
        }
    }
}
