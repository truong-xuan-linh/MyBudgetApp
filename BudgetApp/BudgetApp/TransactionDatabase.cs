using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SQLite;

namespace BudgetApp
{
    class TransactionDatabase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDatabase()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "TransactionDatabase.db");
                var connection = new SQLiteConnection(path);

                //tao bang
                //connection.CreateTable<DateBudget>();
                connection.CreateTable<DetailTransactionClass>();


                return true;
            }
            catch 
            {
                return false;
                throw;
            }


        }
        public bool AddNewTransaction(DetailTransactionClass Budget)
        {

            try { 


                string path = System.IO.Path.Combine(folder, "TransactionDatabase.db");
                var connection = new SQLiteConnection(path);
                connection.Insert(Budget);
                return true;
            }
            catch 
            {
                return false;
                
            } 

        }
        public List<DetailTransactionClass> GetAllTransactioByYear()
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "TransactionDatabase.db");
                var connection = new SQLiteConnection(path);
                string year = DateTime.Now.Year.ToString();
                return connection.Query<DetailTransactionClass>("select * from DetailTransactionClass where transactionDay like '%" + year+"'");
                
            }
            catch
            {
                return null;
            }
        }
        public List<TransactionDateClass> GetTransactionByMonth()
        {
            try { 
                TransactionDatabase db = new TransactionDatabase();
            
                List<TransactionDateClass> dateTransaction = new List<TransactionDateClass>();

                TransactionDateClass Thang1 = new TransactionDateClass() { date = "1/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang2 = new TransactionDateClass() { date = "2/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang4 = new TransactionDateClass() { date = "4/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang5 = new TransactionDateClass() { date = "5/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang6 = new TransactionDateClass() { date = "6/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang7 = new TransactionDateClass() { date = "7/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang8 = new TransactionDateClass() { date = "8/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang9 = new TransactionDateClass() { date = "9/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang10 = new TransactionDateClass() { date = "10/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang11 = new TransactionDateClass() { date = "11/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang12 = new TransactionDateClass() { date = "12/2021", income = 1000000, expense = -50000 };
                TransactionDateClass Thang3 = new TransactionDateClass() { date = "3/2021", income = 1000000, expense = -50000 };

                TransactionDateClass[] allDate = { Thang12, Thang11, Thang10, Thang9, Thang8, Thang7, Thang6, Thang5, Thang4, Thang3, Thang2, Thang1 };

                
                List<DetailTransactionClass> allTransaction = GetAllTransactioByYear();
                int checkMonth = 12;
                
                foreach (TransactionDateClass date in allDate)
                {
                    int income = 0;
                    int expense = 0;
                    int check = 0;
                    foreach (DetailTransactionClass transaction in allTransaction)
                    {
                        DateTime transactionDay = DateTime.ParseExact(transaction.transactionDay, "d/M/yyyy", CultureInfo.InvariantCulture); ;
                        
                        string month = transactionDay.Month.ToString();
                        if (month == checkMonth.ToString())
                        {
                            check = 1;
                            date.Add(transaction);
                        }
                    }
                    if(check ==1)
                    {
                        dateTransaction.Add(date);
                        check = 0;
                    }
                    checkMonth = checkMonth - 1;
                }
                return dateTransaction;
            }
            catch
            {
                return null;
            }
        }
        public bool UpdateTransaction(DetailTransactionClass Budget)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "TransactionDatabase.db");
                var connection = new SQLiteConnection(path);
                connection.Update(Budget);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOnebook(DetailTransactionClass Budget)
        {
            try
            {
                string path = System.IO.Path.Combine(folder, "TransactionDatabase.db");
                var connection = new SQLiteConnection(path);
                connection.Delete(Budget);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
