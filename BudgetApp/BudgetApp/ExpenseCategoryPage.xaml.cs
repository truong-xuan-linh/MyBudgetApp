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

    public partial class ExpenseCategoryPage : ContentPage
    {
        TransactionDatabase db = new TransactionDatabase();
        List<CategoryClass> categoryClasses = new List<CategoryClass>();
        //MyFirebaseAuthentication myAuth;
        string uID;
        public ExpenseCategoryPage()
        {
            
            InitializeComponent();
            uID = db.GetLoginCheck()[0].userID;
            
            categoryClasses = db.GetCategoryClasses("Expense");
            LstExpense.ItemsSource = categoryClasses;
            
        }
        
        
        private void AddCateBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCategoryPage("Expense"));
        }

        private void LstExpense_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CategoryClass category = (CategoryClass)LstExpense.SelectedItem;
            Navigation.PushAsync(new EditCategoryPage(category));
        }
    }
}