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
        MyFirebaseAuthentication myAuth;
        public AccountPage()
        {
            InitializeComponent();
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            EmailLabel.Text = myAuth.GetUname();
        }

        private void SignOutBtn_Clicked(object sender, EventArgs e)
        {
            var signOut = myAuth.SignOut();
            if (signOut)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }

        private void CurrencyBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}