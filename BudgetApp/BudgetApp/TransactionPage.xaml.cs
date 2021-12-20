using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPage : ContentPage
    {
        List<DateBudget> dateBudget = new List<DateBudget>();
        public TransactionPage()
        {
            InitializeComponent();
            DetailBudgetInit();
            IncomeExpenseList.ItemsSource = dateBudget;
        }
        void DetailBudgetInit()
        {
            DateBudget Thang1 = new DateBudget("1/2021") { Income = 1000000, Expense = -50000 };
            Thang1.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay="1/1/2021"});
            Thang1.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay="5/1/2021" });
            Thang1.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay="16/1/2021" });
            dateBudget.Add(Thang1);

            DateBudget Thang2 = new DateBudget("2/2021") { Income = 1000000, Expense = -50000 };
            Thang2.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang2.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang2.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang2);

            DateBudget Thang3 = new DateBudget("3/2021") { Income = 1000000, Expense = -50000 };
            Thang3.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang3.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang3.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang3);

            DateBudget Thang4 = new DateBudget("4/2021") { Income = 1000000, Expense = -50000 };
            Thang4.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang4.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang4.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang4);

            DateBudget Thang5 = new DateBudget("5/2021") { Income = 1000000, Expense = -50000 };
            Thang5.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang5.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang5.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang5);

            DateBudget Thang6 = new DateBudget("6/2021") { Income = 1000000, Expense = -50000 };
            Thang6.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang6.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang6.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang6);

            DateBudget Thang7 = new DateBudget("7/2021") { Income = 1000000, Expense = -50000 };
            Thang7.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang7.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang7.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang7);

            DateBudget Thang8 = new DateBudget("8/2021") { Income = 1000000, Expense = -50000 };
            Thang8.Add(new DetailBudget("Salary") { CategoryColor = "Blue", Icon = "salaryicon.png", CategoryMoney = 100000, CategoryDay = "1/1/2021" });
            Thang8.Add(new DetailBudget("Shopping") { CategoryColor = "Red", Icon = "shoppingicon.png", CategoryMoney = -10000, CategoryDay = "1/1/2021" });
            Thang8.Add(new DetailBudget("Food") { CategoryColor = "Red", Icon = "foodicon.png", CategoryMoney = -50000, CategoryDay = "1/1/2021" });
            dateBudget.Add(Thang8);

        }
    }

    
}