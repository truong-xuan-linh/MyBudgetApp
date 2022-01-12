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
        string selectedMonth;
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

                        yearList.Add(new ChartEntry((float)(month.income)) { Label = DateTime.ParseExact(month.date, "M/yyyy", CultureInfo.InvariantCulture).Month.ToString(), ValueLabel = month.income.ToString("n0"), Color = SKColor.Parse("#75c7fb") });
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    yearList.Add(new ChartEntry((float)(0)) { Label = i.ToString(), ValueLabel = "0", Color = SKColor.Parse("#75c7fb") });
                }
            }
            columnChart.Chart = new BarChart() { Entries = yearList,LabelTextSize = 30, LabelOrientation = Orientation.Horizontal,LabelColor = SKColor.Parse("#000000") };
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
            if (allTransactionByYear.Count == 0)
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
            bool income = true;

            TransactionDatabase db = new TransactionDatabase();
            List<ChartEntry> monthList = new List<ChartEntry>();
            List<DetailTransactionClass> monthTransaction = db.GetTotalMoneyByMonth(month, year, income);


            string[] colors = { "#48D1CC", "#FFFF00", "#1E90FF", "#8B008B", "#191970", "#0000FF", "#008B8B", "#483D8B", "#F4A460", "#A9A9A9", "#9ACD32", "#8B4513", "#A0522D", "#2E8B57", "#008080", "#DAA520", "#800080", "#A52A2A", "#7B68EE", "#F08080", "#808080", "#DB7093", "#EE82EE", "#FF1493", "#556B2F", "#5F9EA0", "#8A2BE2", "#4B0082", "#9400D3", "#FF7F50", "#7CFC00", "#00FA9A", "#228B22", "#0000CD", "#66CDAA", "#FF00FF", "#00FFFF", "#008000", "#FFA07A", "#00FF00", "#778899", "#FF4500", "#BA55D3", "#4682B4", "#FF8C00", "#C71585", "#D2691E", "#6495ED", "#4169E1", "#696969", "#FF6347", "#3CB371", "#7FFF00", "#6B8E23", "#40E0D0", "#DC143C", "#BDB76B", "#006400", "#FFD700", "#8B0000", "#8FBC8F", "#DA70D6", "#20B2AA", "#32CD32", "#FF69B4", "#B8860B", "#2F4F4F", "#000080", "#00BFFF", "#E9967A", "#00CED1", "#FA8072", "#BC8F8F", "#800000", "#808000", "#9932CC", "#B22222", "#6A5ACD", "#FF0000", "#CD5C5C", "#9370DB" };
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
                        ValueLabel = money.ToString("n0"),
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
            this.globalYear = (string)yearChoose.SelectedItem; ;
            DrawColumnChart(globalYear);
            monthPicker.SelectedIndex = 0;
            MonthPickerInit(globalYear);
            DonutMonthChart();
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
    }
}