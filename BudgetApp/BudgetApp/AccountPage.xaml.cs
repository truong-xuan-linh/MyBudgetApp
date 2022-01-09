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
    public partial class AccountPage : ContentPage
    {
        //MyFirebaseAuthentication myAuth;
        TransactionDatabase db = new TransactionDatabase();
        public AccountPage()
        {
            InitializeComponent();
            //myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            EmailLabel.Text = db.GetLoginCheck()[0].userName;
        }

        private async void SignOutBtn_Clicked(object sender, EventArgs e)
        {

            try
            {
                MyFirebaseAuthentication myAuth;
                myAuth = DependencyService.Get<MyFirebaseAuthentication>();
                await Delete();
                await Add();
                await Update();

                await DeleteCategory();
                await AddCategory();
                await UpdateCategory();

                TransactionDatabase db = new TransactionDatabase();
                db.DeleteAllTransaction();
                db.DeleteAllCategory();
                List<LoginCheckClass> check = db.GetLoginCheck();


                var signOut = myAuth.SignOut();
                if (signOut)
                {
                    check[0].isLogin = false;
                    db.UpdateLoginCheck(check[0]);
                    Application.Current.MainPage = new LoginPage();
                }
            }
            catch
            {
                await DisplayAlert("Failed", "Please check your internet connection to backup before sign out", "OK");
            }

        }

        private void CurrencyBtn_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnSync_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Delete();
                await Add();
                await Update();

                await DeleteCategory();
                await AddCategory();
                await UpdateCategory();

                await DisplayAlert("Successful", "Backup your data successfully", "OK");
            }
            catch
            {
                await DisplayAlert("Failed", "Please check your internet connection", "OK");
            }
            
        }

        public async Task Delete()
        {
            TransactionDatabase db = new TransactionDatabase();
            TransactionFirebase fb = new TransactionFirebase();
            List<DetailTransactionClass> fb_transactions = await fb.GetAllTransaction();
            List<DetailTransactionClass> db_transactions = db.GetAllTransaction();

            List<int> db_bID = new List<int>();

            foreach(DetailTransactionClass transaction in db_transactions)
            {
                db_bID.Add(transaction.bID);
            }

            foreach(DetailTransactionClass transaction in fb_transactions)
            {
                if(db_bID.IndexOf(transaction.bID) == -1)
                {
                    fb.DeleteTransaction(transaction);
                }
            }
        }

        public async Task Add()
        {
            TransactionDatabase db = new TransactionDatabase();
            TransactionFirebase fb = new TransactionFirebase();
            List<DetailTransactionClass> fb_transactions = await fb.GetAllTransaction();
            List<DetailTransactionClass> db_transactions = db.GetAllTransaction();

            List<int> fb_bID = new List<int>();

            foreach (DetailTransactionClass transaction in fb_transactions)
            {
                fb_bID.Add(transaction.bID);
            }

            foreach (DetailTransactionClass transaction in db_transactions)
            {
                if (fb_bID.IndexOf(transaction.bID) == -1)
                {
                    fb.AddNewTransaction(transaction);
                }
            }
        }
        public async Task Update()
        {
            TransactionDatabase db = new TransactionDatabase();
            TransactionFirebase fb = new TransactionFirebase();
            List<DetailTransactionClass> fb_transactions = await fb.GetAllTransaction();
            List<DetailTransactionClass> db_transactions = db.GetAllTransaction();

            foreach (DetailTransactionClass transaction in db_transactions)
            {
                if (fb_transactions.IndexOf(transaction) == -1)
                {
                    fb.UpdateTransaction(transaction);
                }
            }
        }

        public async Task DeleteCategory()
        {
            TransactionDatabase db = new TransactionDatabase();
            TransactionFirebase fb = new TransactionFirebase();
            List<CategoryClass> fb_categories = await fb.GetAllCategory();
            List<CategoryClass> db_categories = db.GetAllCategory();

            List<int> db_cateID = new List<int>();

            foreach (CategoryClass category in db_categories)
            {
                db_cateID.Add(category.cateID);
            }

            foreach (CategoryClass category in fb_categories)
            {
                if (db_cateID.IndexOf(category.cateID) == -1)
                {
                    fb.DeleteCategory(category);
                }
            }
        }

        public async Task AddCategory()
        {
            TransactionDatabase db = new TransactionDatabase();
            TransactionFirebase fb = new TransactionFirebase();
            List<CategoryClass> fb_categories = await fb.GetAllCategory();
            List<CategoryClass> db_categories = db.GetAllCategory();

            List<int> fb_cateID = new List<int>();

            foreach (CategoryClass category in fb_categories)
            {
                fb_cateID.Add(category.cateID);
            }

            foreach (CategoryClass category in db_categories)
            {
                if (fb_cateID.IndexOf(category.cateID) == -1)
                {
                    fb.AddNewCategory(category);
                }
            }
        }
        public async Task UpdateCategory()
        {
            TransactionDatabase db = new TransactionDatabase();
            TransactionFirebase fb = new TransactionFirebase();
            List<CategoryClass> fb_categories = await fb.GetAllCategory();
            List<CategoryClass> db_categories = db.GetAllCategory();

            foreach (CategoryClass category in db_categories)
            {
                if (fb_categories.IndexOf(category) == -1)
                {
                    fb.UpdateCategory(category);
                }
            }
        }
    }
}