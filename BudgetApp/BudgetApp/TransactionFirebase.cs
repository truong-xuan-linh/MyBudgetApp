using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SQLite;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace BudgetApp
{
    class TransactionFirebase
    {
        List<string> allYear = new List<string>();
        FirebaseClient firebase = new FirebaseClient("https://newbudgetapp-vinhlinh-default-rtdb.asia-southeast1.firebasedatabase.app/");
        string year = DateTime.Now.Year.ToString();
        MyFirebaseAuthentication myAuth;

        public async Task<bool> AddNewTransaction(DetailTransactionClass Budget)
        {

            await firebase.Child("Transactions").PostAsync(new DetailTransactionClass()
            {

                bID = Budget.bID,
                categoryType = Budget.categoryType,
                icon = Budget.icon,
                transactionColor = Budget.transactionColor,
                transactionDay = Budget.transactionDay,
                transactionMoney = Budget.transactionMoney,
                transactionName = Budget.transactionName,
                userID = Budget.userID,
                cateID = Budget.cateID
            });
            return true;
        }
        public async Task<bool> UpdateTransaction(DetailTransactionClass transaction)
        {
            try
            {
                var toUpdateTransaction = (await firebase.Child("Transactions")
                    .OnceAsync<DetailTransactionClass>()).Where(a => (a.Object.bID == transaction.bID) &&(a.Object.userID == transaction.userID)).FirstOrDefault();
                await firebase.Child("Transactions")
                    .Child(toUpdateTransaction.Key)
                    .PutAsync(transaction);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTransaction(DetailTransactionClass transaction)
        {
            try
            {
                var toDeleteTransaction = (await firebase.Child("Transactions")
                    .OnceAsync<DetailTransactionClass>()).Where(a => (a.Object.bID == transaction.bID) && (a.Object.userID == transaction.userID)).FirstOrDefault();
                await firebase.Child("Transactions")
                    .Child(toDeleteTransaction.Key)
                    .DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<DetailTransactionClass>> GetAllTransaction()
        {
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            try
            {
                List < DetailTransactionClass > allTransaction = (await firebase
                    .Child("Transactions")
                    .OnceAsync<DetailTransactionClass>())
                    .Where(a => a.Object.userID == myAuth.GetUid())
                    .Select(item => new DetailTransactionClass
                    {
                        bID = item.Object.bID,
                        categoryType = item.Object.categoryType,
                        icon = item.Object.icon,
                        transactionColor = item.Object.transactionColor,
                        transactionDay = item.Object.transactionDay,
                        transactionMoney = item.Object.transactionMoney,
                        transactionName = item.Object.transactionName,
                        userID = item.Object.userID,
                        cateID = item.Object.cateID
                    }).ToList();
                return allTransaction;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> AddNewCategory(CategoryClass category)
        {

            await firebase.Child("Categories").PostAsync(new CategoryClass()
            {

                cateID = category.cateID,
                categoryImg = category.categoryImg,
                categoryName = category.categoryName,
                categoryType = category.categoryType,
                userID = category.userID

            });
            return true;
        }
        public async Task<bool> UpdateCategory(CategoryClass category)
        {
            try
            {
                var toUpdateCategory = (await firebase.Child("Categories")
                    .OnceAsync<CategoryClass>()).Where(a => (a.Object.cateID == category.cateID) & (a.Object.userID == category.userID) ).FirstOrDefault();
                await firebase.Child("Categories")
                    .Child(toUpdateCategory.Key)
                    .PutAsync(category);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(CategoryClass category)
        {
            try
            {
                var toDeleteCategory = (await firebase.Child("Categories")
                    .OnceAsync<CategoryClass>()).Where(a => (a.Object.cateID == category.cateID) &(a.Object.userID == category.userID)).FirstOrDefault();
                await firebase.Child("Categories")
                    .Child(toDeleteCategory.Key)
                    .DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<CategoryClass>> GetAllCategory()
        {
            myAuth = DependencyService.Get<MyFirebaseAuthentication>();
            try
            {

                return (await firebase
                    .Child("Categories")
                    .OnceAsync<CategoryClass>())
                    .Where(a => a.Object.userID == myAuth.GetUid())
                    .Select(item => new CategoryClass
                    {
                        categoryImg = item.Object.categoryImg,
                        cateID = item.Object.cateID,
                        categoryType = item.Object.categoryType,
                        categoryName = item.Object.categoryName,
                        userID = item.Object.userID

                    }).ToList();
            }
            catch
            {
                return null;
            }

        }
    }
}
