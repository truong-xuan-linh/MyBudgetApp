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
            loading.IsBusy = false;
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
        }

        private async void SignUpBtn_Clicked(object sender, EventArgs e)
        {
            loading.IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(uName.Text) || string.IsNullOrEmpty(pWord.Text))
                {
                    loading.IsBusy = false;
                    await DisplayAlert("Sign Up Failed", "Invalid Email or Password, Please try again!", "Ok");
                }
                else
                {
                    string user = await myAuth.SignUpWithEmailAndPassword(uName.Text, pWord.Text);
                    if (user != string.Empty)
                    {
                        Application.Current.MainPage = new LoginPage();
                    }
                    else
                    {
                        loading.IsBusy = false;
                        await DisplayAlert("Sign Up Failed", "Email or Password are incorrect, Please try again!", "Ok");
                    }
                    loading.IsBusy = false;
                    
                }
            }
            catch
            {
                loading.IsBusy = false;
                await DisplayAlert("Sign Up Failed", "Please check your internet connection and try again!", "Ok");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}