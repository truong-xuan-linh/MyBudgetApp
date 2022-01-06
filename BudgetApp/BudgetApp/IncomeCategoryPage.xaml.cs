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
    public partial class IncomeCategoryPage : ContentPage
    {
        TransactionDatabase db = new TransactionDatabase();
        List<CategoryClass> categoryClasses = new List<CategoryClass>();
        public IncomeCategoryPage()
        {
            InitializeComponent();
            categoryClasses = db.GetCategoryClasses("Income");
            LstIncome.ItemsSource = categoryClasses;
        }

        private void AddCateBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCategoryPage("Income"));
        }

        private void LstIncome_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CategoryClass category = (CategoryClass)LstIncome.SelectedItem;
            Navigation.PushAsync(new EditCategoryPage(category));
        }
    }
}