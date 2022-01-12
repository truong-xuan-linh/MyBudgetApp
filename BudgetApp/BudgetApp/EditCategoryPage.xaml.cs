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

        List<CategoryClass> categoryClasses = new List<CategoryClass>();
        TransactionDatabase db = new TransactionDatabase();
        bool IsDuplicate = false;
        string cName;
        CategoryClass category;
        public EditCategoryPage()
        {
            InitializeComponent();
        }
        
        public EditCategoryPage(CategoryClass category)
        {
            InitializeComponent();
            categoryClasses = db.GetAllCategoryClasses();
            this.category = category;
            CateIcon.Source = category.categoryImg;
            CateEntry.Text = category.categoryName;
            cName = category.categoryName;
            Typelbl.Text = category.categoryType;
            
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
        private void CateIcon_Clicked(object sender, EventArgs e)
        {
            category.categoryName = CateEntry.Text;
            Navigation.PushAsync(new SelectCateIconPage(category));
        }

        private async void EditCateBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CateEntry.Text) || (TexInput.HasError == true))
            {
                DisplayAlert("Fail", "Edit fail", "OK");
            }
            else
            {
                category.categoryName = CateEntry.Text;
                TransactionDatabase db = new TransactionDatabase();
                if (db.UpdateCategory(category))
                {
                    List<DetailTransactionClass> updateTransactionLst = db.GetTransactionByCateID(category.cateID.ToString());
                    foreach (var a in updateTransactionLst)
                    {
                        a.transactionName = category.categoryName;
                        a.icon = category.categoryImg;
                        db.UpdateTransaction(a);
                    }
                    //await DisplayAlert("Successful", "Update category successfully", "OK");
                    Application.Current.MainPage = new AppShell(category.categoryType);

                }
                else
                {
                    DisplayAlert("Fail", "Update category failed", "OK");
                }
               
            }
        }

        private void CancelCateBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell(category.categoryType);
        }

        private async void DeleteCateBtn_Clicked(object sender, EventArgs e)
        {
            TransactionDatabase db = new TransactionDatabase();
            List<DetailTransactionClass> deleteTransactionLst = db.GetTransactionByCateID(category.cateID.ToString());
            int lstTranCount = deleteTransactionLst.Count;
            if (lstTranCount == 0)
            {
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
            else
            {
                bool delete = await DisplayAlert("Delete category", "Category " + category.categoryName + " has " + lstTranCount.ToString() + " transaction. Do you want to delete!!!", "Delete", "Cancel");
                if (delete)
                {
                    if(db.DeleteCategory(category))
                    {
                        foreach (var a in deleteTransactionLst)
                        {
                            db.DeleteTransaction(a);
                        }
                        //await DisplayAlert("Successful", "Delete category successfully", "OK");
                        Application.Current.MainPage = new AppShell(category.categoryType);
                    }
                    else
                    {
                        DisplayAlert("Fail", "Delete category failed", "OK");
                    }
                }    
            }
        }

        private void CateEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            if (CateEntry.Text != cName)
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
            else
            {
                TexInput.HasError = false;
            }
            
        }
    }
}