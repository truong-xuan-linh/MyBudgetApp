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
            var user = myAuth.SignUpWithEmailAndPassword(uName.Text, pWord.Text);
            if (user != null)
            {
                await DisplayAlert("Success", "Account successfully created", "Ok");
                var signout = myAuth.SignOut();

                if (signout)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("ERROR", "Something went wrong, plese try again", "Ok");
                }
            }
        }
    }
}