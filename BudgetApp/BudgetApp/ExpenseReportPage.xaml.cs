using Microcharts;
using SkiaSharp;
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
    public partial class ExpenseReportPage : ContentPage
    {
        List<string> allYear = new List<string>();
        string globalYear = DateTime.Now.Year.ToString();
        List<string> ctgrNames = new List<string>();
        string selectedMonth;
        public ExpenseReportPage()
        {
            InitializeComponent();
            if (checkEmpty())
            {
                Navigation.PushAsync(new EmptyPage());
            } 
            else
            {
                TransactionDatabase db = new TransactionDatabase();
                List<CategoryClass> category = db.GetAllCategoryClasses();
                foreach (CategoryClass ctgr in category)
                {
                    ctgrNames.Add(ctgr.categoryName);
                }
                allYear.Add(globalYear);
                YearPickerInit();
                yearPicker.SelectedItem = globalYear;
                MonthPickerInit(globalYear);
                monthPicker.SelectedIndex = 0;

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
            for (int i = 1; i <= 12; i++)
            {
                bool flag = false;
                foreach (TransactionDateClass month in dateTransaction)
                {
                    if ((i.ToString() + "/" + globalYear) == month.date)
                    {

                        yearList.Add(new ChartEntry((float)(month.expense)) { Label = DateTime.ParseExact(month.date, "M/yyyy", CultureInfo.InvariantCulture).Month.ToString(), ValueLabel = month.expense.ToString("n0"), Color = SKColor.Parse("#ff3847") });
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    yearList.Add(new ChartEntry((float)(0)) { Label = i.ToString(), ValueLabel = "0", Color = SKColor.Parse("#ff3847") });
                }
            }
            columnChart.Chart = new BarChart() { Entries = yearList, LabelOrientation = Orientation.Horizontal, LabelTextSize = 30, LabelColor = SKColor.Parse("#000000") };
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
            DrawColumnChart(globalYear);
        }
        void MonthPickerInit(string year)
        {
            List<int> allMonth = new List<int>();
            TransactionDatabase db = new TransactionDatabase();
            List<DetailTransactionClass> allTransactionByYear = db.GetAllTransactioByYear(year);
            if(allTransactionByYear.Count == 0)
            {
                allMonth.Add(DateTime.Now.Month);
            }
            foreach (DetailTransactionClass transaction in allTransactionByYear)
            {
                DateTime day = DateTime.ParseExact(transaction.transactionDay, "d/M/yyyy", CultureInfo.InvariantCulture);
                int pos = allMonth.IndexOf(day.Month);
                if (pos == -1)
                {
                    allMonth.Add(day.Month);
                }
            }
            if ((year == DateTime.Now.Month.ToString()) && (allMonth.Contains(DateTime.Now.Month) == false))
            {
                allMonth.Add(DateTime.Now.Month);
            }
            allMonth.Sort();
            allMonth.Reverse();
            List<string> allMonthString = new List<string>();
            foreach (int month in allMonth)
            {
                allMonthString.Add(month.ToString() + "/" + year);
            }
            selectedMonth = allMonthString[0];
            monthPicker.ItemsSource = allMonthString;
            DonutMonthChart();
        }

        void DonutMonthChart()
        {
            string month = DateTime.ParseExact(selectedMonth, "M/yyyy", CultureInfo.InvariantCulture).Month.ToString();
            string year = DateTime.ParseExact(selectedMonth, "M/yyyy", CultureInfo.InvariantCulture).Year.ToString();
            bool income = false;

            TransactionDatabase db = new TransactionDatabase();
            List<ChartEntry> monthList = new List<ChartEntry>();
            List<DetailTransactionClass> monthTransaction = db.GetTotalMoneyByMonth(month, year, income);


            string[] colors = { "#800080", "#DA70D6", "#1E90FF", "#7B68EE", "#FF6347", "#DB7093", "#4B0082", "#D2691E", "#191970", "#00CED1", "#008B8B", "#808000", "#FF7F50", "#4169E1", "#BDB76B", "#0000CD", "#A9A9A9", "#FF8C00", "#FF0000", "#778899", "#FF4500", "#8A2BE2", "#800000", "#9932CC", "#9370DB", "#7FFF00", "#008080", "#A0522D", "#48D1CC", "#9ACD32", "#A52A2A", "#B22222", "#5F9EA0", "#BC8F8F", "#F4A460", "#CD5C5C", "#4682B4", "#BA55D3", "#556B2F", "#8B008B", "#008000", "#696969", "#32CD32", "#FF00FF", "#9400D3", "#2E8B57", "#8FBC8F", "#6495ED", "#EE82EE", "#20B2AA", "#DAA520", "#808080", "#B8860B", "#66CDAA", "#000080", "#E9967A", "#FA8072", "#40E0D0", "#FF1493", "#00FFFF", "#228B22", "#7CFC00", "#3CB371", "#6B8E23", "#6A5ACD", "#2F4F4F", "#FFFF00", "#00BFFF", "#00FA9A", "#006400", "#C71585", "#8B0000", "#F08080", "#FF69B4", "#FFD700", "#0000FF", "#483D8B", "#FFA07A", "#DC143C", "#00FF00", "#8B4513" };
            int i = 0;
            foreach (string name in ctgrNames)
            {
                int k = 0;
                int money = 0;
                foreach (DetailTransactionClass transaction in monthTransaction)
                {
                    if (transaction.transactionName == name)
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
                        ValueLabel = money.ToString("n0"),
                        ValueLabelColor = SKColor.Parse(colors[i]),
                        TextColor = SKColor.Parse("#000000")
                    });
                    k = 0;
                    i = i + 1;
                }
                
            }
            donutChart.Chart = new DonutChart() { Entries = monthList, LabelTextSize = 30 };


        }

        private void monthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var monthChoose = (Picker)sender;
            int lineChoose = monthChoose.SelectedIndex;
            if (lineChoose >= 0)
            {
                selectedMonth = (string)monthChoose.SelectedItem;
            }
            DonutMonthChart();
        }

        private void yearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var yearChoose = (Picker)sender;
            int lineChoose = yearChoose.SelectedIndex;
            this.globalYear = (string)yearChoose.SelectedItem;
            MonthPickerInit(globalYear);
            monthPicker.SelectedIndex = 0;
            DrawColumnChart(globalYear);
            DonutMonthChart();
        }
    }
}