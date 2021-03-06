using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BudgetApp;
using System.Collections.Generic;

namespace BudgetApp
{
    
    public partial class App : Application
    {
       // MyFirebaseAuthentication myAuth;
        

        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTU0NDEwQDMxMzkyZTM0MmUzMGRlRjJCL1k2SHNKSXcrMDdGbUJVc2F4T1c4TU15dzMrc1Vxck5GcjN1eEU9NTU0NDEwQDMxMzkyZTM0MmUzMGRlRjJCL1k2SHNKSXcrMDdGbUJVc2F4T1c4TU15dzMrc1Vxck5GcjN1eEU9");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTYyMDQxQDMxMzkyZTM0MmUzMEdsbjU4NzJHMWxQOXBwT1RTTzF1a0VoVWdhSnZlYTViby9LWG85ZXNqbU09");
            TransactionDatabase db = new TransactionDatabase();
            Console.WriteLine(db.CreateDatabase());
            db.CateIconClassInit();
            //myAuth = DependencyService.Get<MyFirebaseAuthentication>();

            List<LoginCheckClass> check = db.GetLoginCheck();

            if(check != null && check.Count != 0)
            {

                if(check[0].isLogin)
                {

                    MainPage = new AppShell();
                }
                else
                {
                    MainPage = new LoginPage();
                }
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
