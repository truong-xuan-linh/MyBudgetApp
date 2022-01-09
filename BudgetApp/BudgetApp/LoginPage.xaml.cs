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
    public partial class LoginPage : ContentPage
    {
        MyFirebaseAuthentication myAuth;
        public LoginPage()
        {
            InitializeComponent();
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
        }


        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string token = await myAuth.LoginWithEmailAndPassword(uName.Text, pWord.Text);
                if (token != string.Empty)
                {
                    

                    TransactionDatabase db = new TransactionDatabase();
                    TransactionFirebase fb = new TransactionFirebase();

                    List<DetailTransactionClass> restoreTransaction = await fb.GetAllTransaction();
                    List<LoginCheckClass> checkLogin = db.GetLoginCheck();

                    if(checkLogin == null || checkLogin.Count == 0)
                    {
                        Console.WriteLine(db.AddNewLoginCheck(new LoginCheckClass() { isLogin = true, userID = myAuth.GetUid(), userName = myAuth.GetUname() }));
                    }
                    else
                    {
                        
                        checkLogin[0].userID = myAuth.GetUid();
                        checkLogin[0].isLogin = true;
                        checkLogin[0].userName = myAuth.GetUname();
                        db.UpdateLoginCheck(checkLogin[0]);
                        
                    }

                    foreach (DetailTransactionClass transaction in restoreTransaction)
                    {
                        db.AddNewTransaction(transaction);
                    }

                    List<CategoryClass> restoreCategories = await fb.GetAllCategory();
                    foreach (CategoryClass category in restoreCategories)
                    {
                        db.AddNewCategory(category);
                    }
                    
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Sign in Failed", "Email or Password are incorrect, Please try again!", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Sign in Failed", "Please check your internet connection and try again!", "Ok");

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Application.Current.MainPage = new SignupPage();
      
        }
    }
}