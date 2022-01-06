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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTU0NDEwQDMxMzkyZTM0MmUzMGRlRjJCL1k2SHNKSXcrMDdGbUJVc2F4T1c4TU15dzMrc1Vxck5GcjN1eEU9NTU0NDEwQDMxMzkyZTM0MmUzMGRlRjJCL1k2SHNKSXcrMDdGbUJVc2F4T1c4TU15dzMrc1Vxck5GcjN1eEU9");

            TransactionDatabase db = new TransactionDatabase();
            db.CreateDatabase();

            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            if (myAuth.IsSignIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
            //MainPage = new NavigationPage(new SelectCateIconPage());
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
