using System;
using System.Collections.Generic;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Financer
{
    public static class FinancerModel
    {
        public static readonly SQLiteConnection DB = new SQLiteConnection (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "financer.db"));

        public static IEnumerable<Person> GetPeople()
        {
            return DB.Table<Person> ();
        }

        public static IEnumerable<Transaction> GetTransactions()
        {
            return DB.Table<Transaction> ();
        }

        public static IEnumerable<Category> GetCategories()
        {
            return DB.Table<Category> ();
        }

        public static IEnumerable<TransactionSender> GetTransactionSenders()
        {
            return DB.Table<TransactionSender> ();
        }

        public static int AddPerson(Person person)
        {
            return DB.Insert (person);
        }

        public static int AddTransaction(Transaction transaction)
        {
            return DB.Insert (transaction);
        }

        public static int AddCategory(Category category)
        {
            return DB.Insert (category);
        }

        public static int AddTransactionSender(TransactionSender sender)
        {
            return DB.Insert (sender);
        }
    }
}

