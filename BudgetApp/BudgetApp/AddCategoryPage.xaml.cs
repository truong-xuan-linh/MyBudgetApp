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
        public AddCategoryPage()
        {
            InitializeComponent();
            
        }
        public AddCategoryPage(string type_p)
        {
            InitializeComponent();
            Pagelbl.Text = "Add new " + type_p + " category";
            type = type_p;
            Typelbl.Text = type;

        }
        public AddCategoryPage(string img_p, string type_p)
        {
            InitializeComponent();
            Pagelbl.Text = "Add new " + type_p + " category";
            img = img_p;
            type = type_p;
            Typelbl.Text = type;
            CateIcon.Source = img;
            
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
                if(db.AddNewCategory(category))
                {
                    await DisplayAlert("Successful", "Add new category successfully", "OK");
                    //await Shell.Current.GoToAsync("//main/Category");
                    Application.Current.MainPage = new AppShell(type);
                    
                }
                else
                {
                    DisplayAlert("Fail", "Add new category failed", "OK");
                }
            }
            else
            {
                DisplayAlert("Fail", "Please type full information", "OK");
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