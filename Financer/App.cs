using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace Financer
{
    public static class App
    {
        public static void Initialize ()
        {
            InitDummyTransactions ();
            CurrentUser = FinancerModel.GetCurrentUser();
        }

        public static Person CurrentUser { get; private set; }

        private static void InitDummyTransactions()
        {
            if (!FinancerModel.GetTransactions ().Any ()) {
                InsertDummyPeople ();
                InsertDummyCategories ();
                InsertDummyTransactions ();
            }
        }

        private static void InsertDummyPeople()
        {
            FinancerModel.AddOrUpdate (new Person("Nikola Irinchev") { IsCurrentUser = true });
            FinancerModel.AddOrUpdate (new Person ("Telerik"));
            FinancerModel.AddOrUpdate (new Person("Mrusnoto"));
            FinancerModel.AddOrUpdate (new Person("Elena Kufova"));
            FinancerModel.AddOrUpdate (new Person("Telerik Cafeteria"));
        }

        private static void InsertDummyCategories()
        {
            FinancerModel.AddOrUpdate (new Category ("Заплата", "bla bla bla", UIColor.Red));
            FinancerModel.AddOrUpdate (new Category ("Наем", "bla bla bla", UIColor.Green));
            FinancerModel.AddOrUpdate (new Category ("Храна", "bla bla bla", UIColor.Blue));
        }

        private static void InsertDummyTransactions ()
        {
            GenerateTransaction ("Заплата за Юни", 2, 1, 1, 2000, new DateTime(2013, 06, 01));
            GenerateTransaction ("Заплата за Юли", 2, 1, 1, 2000, new DateTime(2013, 07, 01));
            GenerateTransaction ("Обяд", 1,3, 3, 5, new DateTime(2013, 06, 01));
            GenerateTransaction ("Обяд", 1,3, 3, 5.3, new DateTime(2013, 06, 01));
            GenerateTransaction ("Обяд", 1,3, 3, 4.6, new DateTime(2013, 06, 02));
            GenerateTransaction ("Обяд", 1,3, 3, 4.7, new DateTime(2013, 06, 03));
            GenerateTransaction ("Обяд", 1,3, 3, 5.9, new DateTime(2013, 06, 04));
            GenerateTransaction ("Обяд", 1,3, 3, 3.5, new DateTime(2013, 06, 05));
            GenerateTransaction ("Обяд", 1,3, 3, 6.2, new DateTime(2013, 06, 08));
            GenerateTransaction ("Обяд", 1,3, 3, 3.5, new DateTime(2013, 06, 09));
            GenerateTransaction ("Обяд", 1,3, 3, 6.2, new DateTime(2013, 06, 10));
            GenerateTransaction ("Обяд", 1,3, 3, 3.5, new DateTime(2013, 06, 11));
            GenerateTransaction ("Обяд", 1,3, 3, 6.2, new DateTime(2013, 06, 12));
            GenerateTransaction ("Обяд", 1,3, 3, 3.5, new DateTime(2013, 06, 15));
            GenerateTransaction ("Обяд", 1,3, 3, 6.2, new DateTime(2013, 06, 16));
            GenerateTransaction ("Обяд", 1,3, 3, 3.5, new DateTime(2013, 06, 17));
            GenerateTransaction ("Обяд", 1,3, 3, 6.2, new DateTime(2013, 06, 18));
            GenerateTransaction ("Обяд", 1,3, 3, 7.8, new DateTime(2013, 06, 19));
            GenerateTransaction ("Хапване", 1, 5, 3, 2, new DateTime(2013, 06, 1));
            GenerateTransaction ("Хапване", 1, 5, 3, 3, new DateTime(2013, 06, 7));
            GenerateTransaction ("Хапване", 1, 5, 3, 5, new DateTime(2013, 06, 12));
            GenerateTransaction ("Хапване", 1, 5, 3, 0.5, new DateTime(2013, 06, 13));
            GenerateTransaction ("Хапване", 1, 5, 3, 2.5, new DateTime(2013, 06, 19));
            GenerateTransaction ("Хапване", 1, 5, 3, 1.5, new DateTime(2013, 06, 21));
            GenerateTransaction ("Хапване", 1, 5, 3, 3.5, new DateTime(2013, 06, 24));
            GenerateTransaction ("Наем за Юни", 1, 4, 2, 600, new DateTime(2013, 06, 01));
            GenerateTransaction ("Наем за Юли", 1, 4, 2, 600, new DateTime(2013, 07, 01));
        }

        private static void GenerateTransaction (string description, int senderId, int receiverId, int categoryId, double amount, DateTime date)
        {
            FinancerModel.AddOrUpdate (new Transaction () {
                CategoryId = categoryId,
                Amount = amount,
                Date = date,
                Description = description,
                ReceiverId = receiverId,
                SenderId = senderId
            });
        }
    }
}

