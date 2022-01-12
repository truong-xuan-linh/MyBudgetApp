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
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AppShell), typeof(AppShell));
            NavigationPage.SetHasNavigationBar(this, false);
            
        }
        public AppShell(string CateType)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            if (CateType == "Income")
            {
                main.CurrentItem = IncomeItem;
            }    
            else if (CateType == "Expense")
            {
                main.CurrentItem = ExpenseItem;
            }
            else
            {
                main.CurrentItem = AccountItem;
            }
        }
    }
}