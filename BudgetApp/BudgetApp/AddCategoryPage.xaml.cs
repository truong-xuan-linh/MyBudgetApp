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
        bool IsDuplicate = false;
        TransactionDatabase db = new TransactionDatabase();
        List<CategoryClass> categoryClasses = new List<CategoryClass>();
        string userID;
        public AddCategoryPage()
        {
            InitializeComponent();
            
        }
        public AddCategoryPage(string type_p)
        {
            InitializeComponent();
            categoryClasses = db.GetAllCategoryClasses();
            Pagelbl.Text = "Add new " + type_p + " category";
            type = type_p;
            if (CateIcon.Source is Xamarin.Forms.FileImageSource)
            {
                Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)CateIcon.Source;
                img = objFileImageSource.File;
            }
            Typelbl.Text = type;
            List<LoginCheckClass> loginCheck = db.GetLoginCheck();
            userID = loginCheck[0].userID;
        }
        public bool checkDuplicate(string entryCateName)
        {
            
            foreach (CategoryClass cate in categoryClasses)
            {
                if (cate.categoryName == entryCateName)
                {
                    return true;
                }
            }
            return false;
        }
        public AddCategoryPage(string img_p, string type_p, string entry_p)
        {
            InitializeComponent();
            categoryClasses = db.GetAllCategoryClasses();
            IsDuplicate = checkDuplicate(CateEntry.Text);
            if (IsDuplicate)
            {
                TexInput.HasError = true;
            }
            Pagelbl.Text = "Add new " + type_p + " category";
            img = img_p;
            type = type_p;
            CateEntry.Text = entry_p;
            Typelbl.Text = type;
            CateIcon.Source = img;
            List<LoginCheckClass> loginCheck = db.GetLoginCheck();
            userID = loginCheck[0].userID;
        }
        
        private async void AddCateBtn_Clicked(object sender, EventArgs e)
        {
            if ((img == "questionicon.png") || (string.IsNullOrEmpty(CateEntry.Text) == true) || (TexInput.HasError == true))
            {
                await DisplayAlert("Fail", "Add fail", "OK");
            }
            else
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
                    //await DisplayAlert("Successful", "Add new category successfully", "OK");
                    Application.Current.MainPage = new AppShell(type);
                }
                else
                {
                    await DisplayAlert("Fail", "Add new category failed", "OK");
                }
                
            }
        }

        private void CateIcon_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectCateIconPage(type, Addflag, CateEntry.Text));
        }

        private void CancelCateBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell(type);
        }

        private void CateEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDuplicate = checkDuplicate(CateEntry.Text);
            if (IsDuplicate)
            {
                TexInput.HasError = true;
            }
            else
            {
                TexInput.HasError = false;
            }
        }
    }
}