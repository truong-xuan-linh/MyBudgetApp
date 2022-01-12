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
    public partial class ResetPasswordPage : ContentPage
    {
        MyFirebaseAuthentication myAuth;
        public ResetPasswordPage(string title)
        {
            InitializeComponent();
            ResetPWPageTiile.Text = title;
            if(title == "Change Password")
            {
                textLbl.IsVisible = false;
                textLbl1.IsVisible = true;
            }
            else
            {
                textLbl.IsVisible = true;
                textLbl1.IsVisible = false;
            }
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
        }

        private async void SubmitResetPass_Clicked(object sender, EventArgs e)
        {
            string email = uName.Text;
            if(string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Please enter your email.", "OK");
                return;
            }
            bool isSend = myAuth.ResetPassword(email);
            if (isSend)
            {
                await DisplayAlert("Reset Password", "Send link in your email.", "OK");
                Application.Current.MainPage = new LoginPage();
            }    
            else
            {
                await DisplayAlert("Reset Password", "link send fail", "OK");
            }    
        }

        private void SignInTap_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }

        private void BackTap_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell("Account");
        }
    }
}