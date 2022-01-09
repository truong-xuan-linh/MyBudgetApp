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
    public partial class AddCategoryPage : ContentPage
    {
        string img;
        String type;
        String Addflag = "Add";
        //MyFirebaseAuthentication myAuth;
        TransactionDatabase db = new TransactionDatabase();
        string userID;
        public AddCategoryPage()
        {
            InitializeComponent();
            //myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            List<LoginCheckClass> loginCheck = db.GetLoginCheck();
            userID = loginCheck[0].userID;
            
        }
        public AddCategoryPage(string type_p)
        {
            InitializeComponent();
            Pagelbl.Text = "Add new " + type_p + " category";
            type = type_p;
            if (CateIcon.Source is Xamarin.Forms.FileImageSource)
            {
                Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)CateIcon.Source;
                //
                // Access the file that was specified:-
                img = objFileImageSource.File;
            }
            Typelbl.Text = type;
            List<LoginCheckClass> loginCheck = db.GetLoginCheck();
            userID = loginCheck[0].userID;

        }
        public AddCategoryPage(string img_p, string type_p)
        {
            InitializeComponent();
            Pagelbl.Text = "Add new " + type_p + " category";
            img = img_p;
            type = type_p;
            Typelbl.Text = type;
            CateIcon.Source = img;
            List<LoginCheckClass> loginCheck = db.GetLoginCheck();
            userID = loginCheck[0].userID;
        }
        
        private async void AddCateBtn_Clicked(object sender, EventArgs e)
        {
            if ((CateIcon.Source.ToString() != "questionicon.png") && (CateEntry.Text != null))
            {
                CategoryClass category = new CategoryClass();
                category.categoryImg = img;
                category.categoryName = CateEntry.Text;
                category.categoryType = type;

                TransactionDatabase db = new TransactionDatabase();
                category.userID = userID;
                Console.WriteLine(userID);
                if (db.AddNewCategory(category))
                {
                    await DisplayAlert("Successful", "Add new category successfully", "OK");
                    //await Shell.Current.GoToAsync("//main/Category");
                    Application.Current.MainPage = new AppShell(type);
                    
                }
                else
                {
                    await DisplayAlert("Fail", "Add new category failed", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fail", "Please type full information", "OK");
            }
        }

        private void CateIcon_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectCateIconPage(type, Addflag));
        }

        private void CancelCateBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell(type);
        }
    }
}