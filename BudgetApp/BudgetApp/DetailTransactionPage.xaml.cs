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
    public partial class DetailTransactionPage : ContentPage
    {
        string categoryTypeName;
        string categoryImgName;
        TransactionDatabase db = new TransactionDatabase();
        List<CategoryTypeClass> categoryListItem = new List<CategoryTypeClass>();
        public DetailTransactionPage()
        {
            InitializeComponent();
            CategoryListInit();
            categoryListt.ItemsSource = categoryListItem;
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

        DetailTransactionClass transaction;
        public DetailTransactionPage(DetailTransactionClass transaction)
        {
            InitializeComponent();
            CategoryListInit();
            categoryListt.ItemsSource = categoryListItem;
            entryMoney.Text = transaction.transactionMoney.ToString();
            datePicker.Date = DateTime.ParseExact(transaction.transactionDay, "d/M/yyyy", CultureInfo.InvariantCulture);
            categoryIcon.Source = transaction.icon;
            chooseCategory.Text = transaction.transactionName;
            categoryTypeName = transaction.categoryType;
            this.transaction = transaction;

        }

        
        private async void Ckecked_Clicked(object sender, EventArgs e)
        {


            string transactionName = chooseCategory.Text;

            transaction.transactionName = transactionName;
            transaction.transactionDay = datePicker.Date.ToString("d/M/yyyy", CultureInfo.InvariantCulture);

            if (categoryIcon.Source is Xamarin.Forms.FileImageSource)
            {
                Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)categoryIcon.Source;
                //
                // Access the file that was specified:-
                categoryImgName = objFileImageSource.File;
            }
            try
            {
                if ((entryMoney.Text != null) && (categoryImgName != "questionicon.png") && entryMoney.Text != "0")
                {
                    transaction.transactionMoney = Int32.Parse(entryMoney.Text);
                    transaction.categoryType = categoryTypeName;
                    transaction.icon = categoryImgName;

                    if (categoryTypeName == "Expense")
                    {
                        transaction.transactionColor = "red";
                    }
                    else
                    {
                        transaction.transactionColor = "blue";
                    }

                    TransactionDatabase db = new TransactionDatabase();
                    if (db.UpdateTransaction(transaction))
                    {
                        await DisplayAlert("Successful", "Update transaction successfully", "OK");

                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        DisplayAlert("Fail", "Update transaction failed", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Fail", "Please type full information", "OK");
                }
            }
            catch
            {
                DisplayAlert("Fail", "Please type full information", "OK");
            }
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

        private void Cancel_Clicked(object sender, EventArgs e)
        {
      
            Navigation.PushAsync(new TransactionPage());
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            TransactionDatabase db = new TransactionDatabase();
            if (db.DeleteTransaction(transaction))
            {
                await DisplayAlert("Successful", "Delete transaction successfully", "OK");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                DisplayAlert("Fail", "Delete transaction failed", "OK");
            }

            
        }

        private void categoryList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CategoryClass budgetSelected = (CategoryClass)categoryListt.SelectedItem;
            
            chooseCategory.Text = budgetSelected.categoryName;
            categoryIcon.Source = budgetSelected.categoryImg;
            categoryTypeName = budgetSelected.categoryType;
        }
    }
}