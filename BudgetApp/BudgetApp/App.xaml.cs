using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BudgetApp;

namespace BudgetApp
{
    public partial class App : Application
    {
        MyFirebaseAuthentication myAuth;
        public App()
        {
            InitializeComponent();
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            if (myAuth.IsSignIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
