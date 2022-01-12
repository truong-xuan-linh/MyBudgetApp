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
            loading.IsBusy = false;
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
        }


        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            loading.IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(uName.Text) || string.IsNullOrEmpty(pWord.Text))
                {
                    loading.IsBusy = false;
                    await DisplayAlert("Log in Failed", "Invalid Email or Password, Please try again!", "Ok");
                }
                else
                {
                    string token = await myAuth.LoginWithEmailAndPassword(uName.Text, pWord.Text);
                    if (token != string.Empty)
                    {
                        TransactionDatabase db = new TransactionDatabase();
                        TransactionFirebase fb = new TransactionFirebase();

                        List<DetailTransactionClass> restoreTransaction = await fb.GetAllTransaction();
                        List<LoginCheckClass> checkLogin = db.GetLoginCheck();

                        if (checkLogin == null || checkLogin.Count == 0)
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
                        db.cateInit();
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        loading.IsBusy = false;
                        await DisplayAlert("Log in Failed", "Email or Password are incorrect, Please try again!", "Ok");
                    }
                }
            }
            catch
            {
                loading.IsBusy = false;
                await DisplayAlert("Log in Failed", "Please check your internet connection and try again!", "Ok");

            }

        }

        

        private void SignUpTap_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupPage();
        }

        private void ForgotTap_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ResetPasswordPage("Forgot Password");
        }
    }
}