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
    public partial class SignupPage : ContentPage
    {
        MyFirebaseAuthentication myAuth;
        public SignupPage()
        {
            InitializeComponent();
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
        }

        private async void SignUpBtn_Clicked(object sender, EventArgs e)
        {
            string user = await myAuth.SignUpWithEmailAndPassword(uName.Text, pWord.Text);
            if (user != string.Empty)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Sign Up Failed", "Email or Password are incorrect, Please try again!", "Ok");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}