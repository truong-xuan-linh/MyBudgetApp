using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoryPage : ContentPage
    {
        
       
        public EditCategoryPage()
        {
            InitializeComponent();
        }
        CategoryClass category;
        public EditCategoryPage(CategoryClass category)
        {
            InitializeComponent();
            CateIcon.Source = category.categoryImg;
            CateEntry.Text = category.categoryName;
            Typelbl.Text = category.categoryType;
            this.category = category;
        }    
        private void CateIcon_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SelectCateIconPage(category));
        }

        private async void EditCateBtn_Clicked(object sender, EventArgs e)
        {
            if (CateEntry.Text != null)
            {
                category.categoryName = CateEntry.Text;
                TransactionDatabase db = new TransactionDatabase();
                if (db.UpdateCategory(category))
                {
                    await DisplayAlert("Successful", "Update category successfully", "OK");
                    Application.Current.MainPage = new AppShell(category.categoryType);

                }
                else
                {
                    DisplayAlert("Fail", "Update category failed", "OK");
                }
            }
            else
            {
                DisplayAlert("Fail", "Please type full information", "OK");
            }
        }

        private void CancelCateBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell(category.categoryType);
        }

        private async void DeleteCateBtn_Clicked(object sender, EventArgs e)
        {
            TransactionDatabase db = new TransactionDatabase();
            if (db.DeleteCategory(category))
            {
                await DisplayAlert("Successful", "Delete category successfully", "OK");
                Application.Current.MainPage = new AppShell(category.categoryType);

            }
            else
            {
                DisplayAlert("Fail", "Delete category failed", "OK");
            }
        }
    }
}