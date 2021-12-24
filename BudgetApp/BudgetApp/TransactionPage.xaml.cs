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
        List<TransactionDateClass> dateTransaction = new List<TransactionDateClass>();
        int totalExpense;
        int totalIncome;
        public TransactionPage()
        {
            InitializeComponent();
            DetailBudgetInit();
            TransactionDatabase db = new TransactionDatabase();
            Income.Text = totalIncome.ToString();
            Expense.Text = totalExpense.ToString();
            totalMoney.Text = db.GetTotalMoney().ToString();
            incomeExpenseList.ItemsSource = dateTransaction;
        }
        void DetailBudgetInit()
        {
            TransactionDatabase db = new TransactionDatabase();
            (dateTransaction,totalIncome,totalExpense) = db.GetTransactionByMonth();
        }

        private void incomeExpenseList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DetailTransactionClass selectedBudget = (DetailTransactionClass)incomeExpenseList.SelectedItem;
            Navigation.PushAsync(new DetailTransactionPage(selectedBudget));
        }
    }

    
}