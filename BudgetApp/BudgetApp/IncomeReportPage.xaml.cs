using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Globalization;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeReportPage : ContentPage
    {
        List<string> allYear = new List<string>();
        List<string> ctgrNames = new List<string>();
        string globalYear = DateTime.Now.Year.ToString();
        public IncomeReportPage()
        {
            InitializeComponent();
            if (checkEmpty())
            {

                Navigation.PushAsync(new EmptyPage());
            }
            else
            {
                TransactionDatabase db = new TransactionDatabase();
                List<CategoryClass> category = db.GetAllCategory();
                foreach (CategoryClass ctgr in category)
                {
                    ctgrNames.Add(ctgr.categoryName);
                }

                YearPickerInit();
                MonthPickerInit(globalYear);
            }  
        }
        bool checkEmpty()
        {
            TransactionDatabase db = new TransactionDatabase();
            List<DetailTransactionClass> dateTransaction = db.GetAllTransaction();
            if (dateTransaction.Count == 0)
            {
                return true;
            }
            return false;
        }
        void DrawColumnChart(string year)
        {
            List<ChartEntry> yearList = new List<ChartEntry>();
            TransactionDatabase db = new TransactionDatabase();

            List<TransactionDateClass> dateTransaction = new List<TransactionDateClass>();
            int totalExpense;
            int totalIncome;
            (dateTransaction, totalIncome, totalExpense) = db.GetTransactionByMonth(year);
            int k = 0;
            dateTransaction.Sort((t1, t2) => 
                {
                    return DateTime.ParseExact(t1.date, "M/yyyy", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(t2.date, "M/yyyy", CultureInfo.InvariantCulture));
                }
            );
            foreach (TransactionDateClass month in dateTransaction)
            {
                yearList.Add(new ChartEntry((float)(month.income)) { Label = month.date, ValueLabel = month.income.ToString(), Color = SKColor.Parse("#3366CC")});
            }
            columnChart.Chart = new BarChart() { Entries = yearList,LabelTextSize = 30, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal,LabelColor = SKColor.Parse("#000000") };
        }
        
        void YearPickerInit()
        {
            yearPicker.Title = globalYear;

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
            DrawColumnChart(globalYear);
        }
        void MonthPickerInit(string year)
        {
            List<int> allMonth = new List<int>();
            TransactionDatabase db = new TransactionDatabase();
            List<DetailTransactionClass> allTransactionByYear = db.GetAllTransactioByYear(year);
            foreach (DetailTransactionClass transaction in allTransactionByYear)
            {
                DateTime day = DateTime.ParseExact(transaction.transactionDay, "d/M/yyyy", CultureInfo.InvariantCulture);
                int pos = allMonth.IndexOf(day.Month);
                if (pos == -1)
                {
                    allMonth.Add(day.Month);
                }
            }
            allMonth.Sort();
            allMonth.Reverse();
            List<string> allMonthString = new List<string>();
            foreach(int month in allMonth)
            {
                allMonthString.Add(month.ToString() + "/" + year);
            }
            monthPicker.Title = allMonthString[0];
            monthPicker.ItemsSource = allMonthString;
            
            DonutMonthChart();
        }

        void DonutMonthChart()
        {
            string month = DateTime.ParseExact(monthPicker.Title, "M/yyyy", CultureInfo.InvariantCulture).Month.ToString();
            string year = DateTime.ParseExact(monthPicker.Title, "M/yyyy", CultureInfo.InvariantCulture).Year.ToString();
            bool income = true;

            TransactionDatabase db = new TransactionDatabase();
            List<ChartEntry> monthList = new List<ChartEntry>();
            List<DetailTransactionClass> monthTransaction = db.GetTotalMoneyByMonth(month, year, income);
            

            string[] colors = { "#CC0000", "#00FF66", "#CC99CC", "#CC99CC", "#CC99CC", "#CC99CC", "#CC99CC" };
            int i = 0;
            foreach (string name in ctgrNames)
            {
                int k = 0;
                int money = 0;
                foreach (DetailTransactionClass transaction in monthTransaction)
                {
                    if(transaction.transactionName == name)
                    {
                        money = money + transaction.transactionMoney;
                        k = 1;
                    }
                }
                if (k == 1)
                {

                    monthList.Add(new ChartEntry(money)
                    {
                        Color = SKColor.Parse(colors[i]),
                        Label = name,
                        ValueLabel = money.ToString(),
                        ValueLabelColor = SKColor.Parse(colors[i]),
                        TextColor = SKColor.Parse("#000000")
                    });
                    k = 0;
                    i = i + 1;
                }
            }

            donutChart.Chart = new DonutChart() { Entries = monthList, LabelTextSize=30};

        }
        private void yearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var yearChoose = (Picker)sender;
            int lineChoose = yearChoose.SelectedIndex;
            if (lineChoose >= 0)
            {
                yearPicker.Title = (string)yearChoose.SelectedItem;
            }
            this.globalYear = yearPicker.Title;
            DrawColumnChart(globalYear);
            MonthPickerInit(globalYear);
            DonutMonthChart();
        }

        private void monthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var monthChoose = (Picker)sender;
            int lineChoose = monthChoose.SelectedIndex;
            if (lineChoose >= 0)
            {
                monthPicker.Title = (string)monthChoose.SelectedItem;
            }
            DonutMonthChart();
        }
    }
}