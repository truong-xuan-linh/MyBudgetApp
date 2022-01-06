using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SQLite;

namespace BudgetApp
{
    class TransactionDatabase
    {
        List<string> allYear = new List<string>();
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        string year = DateTime.Now.Year.ToString();
        string datafolder = "Test1.db";
        public bool CreateDatabase()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);

                connection.CreateTable<DetailTransactionClass>();
                connection.CreateTable<CategoryClass>();


                return true;
            }
            catch 
            {
                return false;
                throw;
            }

            
        }
        public bool AddNewCategory(CategoryClass category)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                connection.Insert(category);
                return true;
            }
            catch 
            {
                return false;

            }
        }    
        public List<CategoryClass> GetCategoryClasses(string type)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);

                return connection.Query<CategoryClass>("select * from CategoryClass where categoryType= '" + type + "'");

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<CategoryClass> GetAllCategoryClasses()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);

                return connection.Table<CategoryClass>().ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateCategory(CategoryClass category)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                connection.Update(category);
                return true;
            }
            catch
            {
                return false;

            }
        }
        public bool DeleteCategory(CategoryClass category)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                connection.Delete(category);
                return true;
            }
            catch
            {
                return false;

            }
        }
        public bool AddNewTransaction(DetailTransactionClass Budget)
        {

            try { 


                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                connection.Insert(Budget);
                return true;
            }
            catch 
            {
                return false;
                
            } 

        }
        public List<DetailTransactionClass> GetAllTransactioByYear(string year)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                
                return connection.Query<DetailTransactionClass>("select * from DetailTransactionClass where transactionDay like '%" + year+"'");
                
            }
            catch
            {
                return null;
            }
        }
        public List<DetailTransactionClass> GetAllTransaction()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                string year = DateTime.Now.Year.ToString();
                return connection.Query<DetailTransactionClass>("select * from DetailTransactionClass where transactionDay is not null");

            }
            catch
            {
                return null;
            }
        }
       
        public int GetTotalMoney()
        {
            int totalMoney = 0;
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                List<DetailTransactionClass> allTransaction =  connection.Table<DetailTransactionClass>().ToList();
                foreach(DetailTransactionClass transaction in allTransaction)
                {
                    if(transaction.categoryType == "Expense")
                    {
                        totalMoney = totalMoney - transaction.transactionMoney;
                    }
                    else
                    {
                        totalMoney = totalMoney + transaction.transactionMoney;
                    }
                }
                return totalMoney;
            }
            catch
            {
                return 0;
            }
        }
        public List<TotalTransactionMonthClass> GetTotalMoneyByMonth(string month, string year, bool income)
        {
            List<TotalTransactionMonthClass> totalTransactionMonth =new List<TotalTransactionMonthClass>();
            string path = System.IO.Path.Combine(folder, datafolder);
            var connection = new SQLiteConnection(path);
            List<DetailTransactionClass> monthTransaction;
            monthTransaction =  connection.Query<DetailTransactionClass>("select * from DetailTransactionClass where transactionDay like '%" + month+"/"+ year + "'");
            string[] categoryNames = { "Shopping", "Salary", "Food" };

            if (income = true)
            {
                foreach (string name in categoryNames)
                {
                    int k = 0;
                    int incomeMoney = 0;
                    foreach (DetailTransactionClass transaction in monthTransaction)
                    {
                        if (transaction.transactionName == name)
                        {
                            incomeMoney = incomeMoney + transaction.transactionMoney;
                            k = 1;
                        }

                    }
                    if (k == 1)
                    {
                        totalTransactionMonth.Add(new TotalTransactionMonthClass() { transactionName = name, totalMoney = (float)incomeMoney });
                    }

                }
            }
            return totalTransactionMonth;
        }
        public (List<TransactionDateClass>, int, int) GetTransactionByMonth(string year)
        {
       
            try {
                
                TransactionDatabase db = new TransactionDatabase();
            
                List<TransactionDateClass> dateTransaction = new List<TransactionDateClass>();

                TransactionDateClass Thang1 = new TransactionDateClass() { date = "1/"+year };
                TransactionDateClass Thang2 = new TransactionDateClass() { date = "2/"+year };
                TransactionDateClass Thang4 = new TransactionDateClass() { date = "4/"+year};
                TransactionDateClass Thang5 = new TransactionDateClass() { date = "5/"+year };
                TransactionDateClass Thang6 = new TransactionDateClass() { date = "6/"+year };
                TransactionDateClass Thang7 = new TransactionDateClass() { date = "7/"+year };
                TransactionDateClass Thang8 = new TransactionDateClass() { date = "8/"+year };
                TransactionDateClass Thang9 = new TransactionDateClass() { date = "9/"+year};
                TransactionDateClass Thang10 = new TransactionDateClass() { date = "10/"+year };
                TransactionDateClass Thang11 = new TransactionDateClass() { date = "11/"+year };
                TransactionDateClass Thang12 = new TransactionDateClass() { date = "12/"+year };
                TransactionDateClass Thang3 = new TransactionDateClass() { date = "3/"+year };

                TransactionDateClass[] allDate = { Thang12, Thang11, Thang10, Thang9, Thang8, Thang7, Thang6, Thang5, Thang4, Thang3, Thang2, Thang1 };

                
                List<DetailTransactionClass> allTransaction = GetAllTransactioByYear(year);
                int checkMonth = 12;
                int totalIncome = 0;
                int totalExpense = 0;
                foreach (TransactionDateClass date in allDate)
                {
                    int income = 0;
                    int expense = 0;
                    int check = 0;
                    foreach (DetailTransactionClass transaction in allTransaction)
                    {
                        DateTime transactionDay = DateTime.ParseExact(transaction.transactionDay, "d/M/yyyy", CultureInfo.InvariantCulture);
                        
                        string month = transactionDay.Month.ToString();
                        if (month == checkMonth.ToString())
                        {
                            check = 1;
                            date.Add(transaction);
                            if(transaction.categoryType == "Expense")
                            {
                                expense = expense + transaction.transactionMoney;
                                totalExpense = totalExpense + transaction.transactionMoney;
                            }
                            else
                            {
                                income = income + transaction.transactionMoney;
                                totalIncome = totalIncome + transaction.transactionMoney;
                            }
                        }
                    }
                    if(check ==1)
                    {
                        date.income = income;
                        date.expense = expense;
                        date.Reverse();
                        dateTransaction.Add(date);
                        check = 0;
                    }
                    checkMonth = checkMonth - 1;
                }
                return (dateTransaction, totalIncome, totalExpense);
            }
            catch
            {
                return (null,0,0);
            }
        }
        public bool UpdateTransaction(DetailTransactionClass transaction)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                connection.Update(transaction);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTransaction(DetailTransactionClass transaction)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, datafolder);
                var connection = new SQLiteConnection(path);
                connection.Delete(transaction);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
