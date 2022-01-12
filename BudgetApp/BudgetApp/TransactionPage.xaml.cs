using System;
using System.Collections.Generic;
using System.Globalization;
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
        List<string> allYear = new List<string>();
        int totalExpense;
        int totalIncome;
        string globalYear = DateTime.Now.Year.ToString();
        CultureInfo current = CultureInfo.CurrentCulture;

        public TransactionPage()
        {
            InitializeComponent();
            DetailBudgetInit();
            allYear.Add(globalYear);
            YearPickerInit();
            yearPicker.SelectedItem = globalYear;
            TransactionDatabase db = new TransactionDatabase();
            Income.Text = totalIncome.ToString("n0");
            Expense.Text = totalExpense.ToString("n0");
            totalMoney.Text = db.GetTotalMoney().ToString("n0");
            incomeExpenseList.ItemsSource = dateTransaction;
        }
        void YearPickerInit()
        {

            TransactionDatabase db = new TransactionDatabase();
            List<DetailTransactionClass> allTransaction = db.GetAllTransaction();
            foreach (DetailTransactionClass transaction in allTransaction)
            {
                DateTime day = DateTime.ParseExact(transaction.transactionDay, "d/M/yyyy", CultureInfo.InvariantCulture);
                int pos = allYear.IndexOf(day.Year.ToString());
                if (pos == -1)
                {
                    allYear.Add(day.Year.ToString());
                }
            }
            allYear.Sort();
            allYear.Reverse();
            yearPicker.ItemsSource = allYear;
        }
        void DetailBudgetInit()
        {
            TransactionDatabase db = new TransactionDatabase();
            string year = DateTime.Now.Year.ToString();
            (dateTransaction,totalIncome,totalExpense) = db.GetTransactionByMonth(globalYear);
        }

        private void incomeExpenseList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DetailTransactionClass selectedBudget = (DetailTransactionClass)incomeExpenseList.SelectedItem;
            Navigation.PushAsync(new DetailTransactionPage(selectedBudget));
        }

        private void yearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var yearChoose = (Picker)sender;
            int lineChoose = yearChoose.SelectedIndex;
            this.globalYear = (string)yearChoose.SelectedItem; ;
            DetailBudgetInit();
            Income.Text = totalIncome.ToString();
            Expense.Text = totalExpense.ToString();
            incomeExpenseList.ItemsSource = dateTransaction;
        }
    }

    
}