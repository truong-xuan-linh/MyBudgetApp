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
            string token = await myAuth.LoginWithEmailAndPassword(uName.Text, pWord.Text);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or Password are incorrect, Please try again!", "Ok");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Application.Current.MainPage = new SignupPage();
            
        }
    }
}