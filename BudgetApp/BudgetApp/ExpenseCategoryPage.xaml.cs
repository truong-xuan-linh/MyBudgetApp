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
        public ExpenseCategoryPage()
        {
           
            InitializeComponent();
            cateInit();
            categoryClasses = db.GetCategoryClasses("Expense");
            LstExpense.ItemsSource = categoryClasses;
            
        }
        private void cateInit()
        {
            List<CategoryClass> CateLst = db.GetAllCategoryClasses();
            if (CateLst.Count == 0)
            {
                CategoryClass category = new CategoryClass();
                category.categoryType = "Income";
                category.categoryImg = "icon_0.png";
                category.categoryName = "Scholarship";
                db.AddNewCategory(category);

                category.categoryImg = "icon_26.png";
                category.categoryName = "Salary";
                db.AddNewCategory(category);

                category.categoryImg = "icon_48.png";
                category.categoryName = "Save Money";
                db.AddNewCategory(category);

                category.categoryType = "Expense";
                category.categoryImg = "icon_12.png";
                category.categoryName = "Food";
                db.AddNewCategory(category);

                category.categoryImg = "icon_34.png";
                category.categoryName = "Water Bill";
                db.AddNewCategory(category);

                category.categoryImg = "icon_36.png";
                category.categoryName = "Gas Bill";
                db.AddNewCategory(category);
            }    
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