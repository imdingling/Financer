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

        public static int AddOrUpdate(Person person)
        {
            if (person.Id == 0) {
                return DB.Insert (person);
            } else {
                return DB.Update (person);
            }
        }

        public static int AddOrUpdate(Transaction transaction)
        {
            if (transaction.Id == 0) {
                return DB.Insert (transaction);
            } else {
                return DB.Update (transaction);
            }
        }

        public static int AddOrUpdate(Category category)
        {
            if (category.Id == 0) {
                return DB.Insert (category);
            } else {
                return DB.Update (category);
            }
        }

        public static int Delete(Category category)
        {
            var result = 0;
            if (category != null) {            
                DB.BeginTransaction ();
                var transactionsToDelete = GetTransactions ().Where (t => t.CategoryId == category.Id).ToArray ();
                foreach (var transaction in transactionsToDelete) {
                    result += DB.Delete (transaction);
                }

                result += DB.Delete (category);
                DB.Commit ();
            }

            return result;
        }

        public static int Delete(Person person)
        {
            var result = 0;
            if (person != null) {
                DB.BeginTransaction ();
                var transactionsToDelete = GetTransactions ().Where (t => t.ReceiverId == person.Id || t.SenderId == person.Id).ToArray ();
                foreach (var transaction in transactionsToDelete) {
                    result += DB.Delete (transaction);
                }

                result += DB.Delete (person);
                DB.Commit ();
            }

            return result;
        }

        public static int Delete(Transaction transaction)
        {
            return DB.Delete (transaction);
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

