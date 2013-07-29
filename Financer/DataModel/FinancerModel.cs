using System;
using System.Linq;
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

        public static IEnumerable<Person> GetOtherPeople()
        {
            return DB.Table<Person> ().Where(p => p.IsCurrentUser == false);
        }

        public static IEnumerable<Person> GetAllPeople()
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

        public static Person GetCurrentUser()
        {
            return DB.Table<Person> ().FirstOrDefault (p => p.IsCurrentUser);
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

        public static double GetBalance(Person person)
        {
            return GetTransactions().Where (trans => trans.SenderId == person.Id).Sum (trans => trans.Amount) - 
                   GetTransactions().Where (trans => trans.ReceiverId == person.Id).Sum (trans => trans.Amount);
        }

        public static double GetBalance(Category category)
        {
            return GetTransactions ().Where (trans => trans.CategoryId == category.Id).Sum (trans => (trans.IsInbound ? 1 : -1) * trans.Amount);
        }
    }
}

