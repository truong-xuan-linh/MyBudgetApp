using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetApp
{
    class CategoryTypeClass: List<CategoryClass>
    {
        public string categoryType { get; set; }
        public CategoryTypeClass(string categoryType)
        {
            this.categoryType = categoryType;

        }
    }
}
