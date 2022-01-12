using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.DeviceOrientation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransactionPage : ContentPage
    {
        string categoryTypeName;
        string categoryImgName;
        string userID; 
        TransactionDatabase db = new TransactionDatabase();
        List<CategoryTypeClass> categoryListItem = new List<CategoryTypeClass>();
        DetailTransactionClass newBudget = new DetailTransactionClass();
        
        public AddTransactionPage()
        {
            InitializeComponent();
            List<LoginCheckClass> loginCheck = db.GetLoginCheck();
            userID = loginCheck[0].userID;
            CategoryListInit();
            categoryList.ItemsSource = categoryListItem;
        }
        void CategoryListInit()
        {
            CategoryTypeClass Income = new CategoryTypeClass("Income");
            foreach (var a in db.GetCategoryClasses("Income"))
                Income.Add(a);
            
            categoryListItem.Add(Income);

            CategoryTypeClass Expense = new CategoryTypeClass("Expense");
            foreach (var a in db.GetCategoryClasses("Expense"))
                Expense.Add(a);
            categoryListItem.Add(Expense);
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Action<double> callback = input => MyDraggableView.HeightRequest = input;
            double startHeight = 300;
            double endiendHeight = 0;
            uint rate = 32;
            uint length = 500;
            Easing easing = Easing.SinOut;
            MyDraggableView.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
        }

        private void Category_Clicked(object sender, EventArgs e)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;


            if (MyDraggableView.Height == 0)
            {
                Action<double> callback = input => MyDraggableView.HeightRequest = input;
                double startHeight = 0;
                double endHeight = 300;
                uint rate = 32;
                uint length = 500;
                Easing easing = Easing.CubicOut;
                MyDraggableView.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else
            {
                Action<double> callback = input => MyDraggableView.HeightRequest = input;
                double startHeight = 300;
                double endiendHeight = 0;
                uint rate = 32;
                uint length = 500;
                Easing easing = Easing.SinOut;
                MyDraggableView.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);

            }
        }

        private async void Ckecked_Clicked(object sender, EventArgs e)
        {
            
            newBudget.transactionName = chooseCategory.Text;

            
            newBudget.transactionDay = datePicker.Date.ToString("d/M/yyyy", CultureInfo.InvariantCulture);

            if (categoryIcon.Source is Xamarin.Forms.FileImageSource)
            {
                Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)categoryIcon.Source;
                categoryImgName = objFileImageSource.File;
            }
            try
            {
                if ((entryMoney.Value.ToString() != null) && (categoryImgName != "questionicon.png") && entryMoney.Value.ToString() != "0")
                {
                    newBudget.transactionMoney = Int32.Parse(entryMoney.Value.ToString());

                    newBudget.icon = categoryImgName;

                    if (categoryTypeName == "Expense")
                    {
                        newBudget.transactionColor = "#ff3847";
                        newBudget.categoryType = "Expense";
                    }
                    else
                    {

                        newBudget.transactionColor = "#3366CC";
                        newBudget.categoryType = "Income";
                    }

                    TransactionDatabase db = new TransactionDatabase();

                    newBudget.userID = userID;

                    if (db.AddNewTransaction(newBudget))
                    {
                        //await DisplayAlert("Successful", "Add new transaction successfully", "OK");

                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                       await DisplayAlert("Fail", "Add new transaction failed", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Fail", "Please type full information", "OK");
                }
            }
            catch{
                await DisplayAlert("Fail", "Please type full information", "OK");
            }
        }
        private void CategoryList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CategoryClass cateSelected = (CategoryClass)categoryList.SelectedItem;

            Action<double> callback = input => MyDraggableView.HeightRequest = input;
            double startHeight = 300;
            double endiendHeight = 0;
            uint rate = 32;
            uint length = 500;
            Easing easing = Easing.SinOut;
            MyDraggableView.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
            if(cateSelected.categoryType == "Income")
            {
                entryMoney.TextColor = Color.FromHex("#2193b0");
            }
            else
            {
                entryMoney.TextColor = Color.FromHex("#ff3847");
            }
            newBudget.cateID = cateSelected.cateID.ToString();
            chooseCategory.Text = cateSelected.categoryName;
            categoryIcon.Source = cateSelected.categoryImg;
            categoryTypeName = cateSelected.categoryType;
 
        }

    }
}