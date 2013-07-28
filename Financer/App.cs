using System;
using System.Linq;
using System.Collections.Generic;

namespace Financer
{
    public static class App
    {
        public static void Initialize ()
        {
            InitDummyTransactions ();
            CurrentUser = FinancerModel.GetPeople ().First ();
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
            FinancerModel.AddPerson (new Person("Current", "Currentov"));
            FinancerModel.AddPerson (new Person ("Pesho", "Peshev"));
            FinancerModel.AddPerson (new Person("Niki", "Nikov"));
            FinancerModel.AddPerson (new Person("Penka", "Penkova"));
            FinancerModel.AddPerson (new Person("Tosho", "Toshev"));
        }

        private static void InsertDummyCategories()
        {
            FinancerModel.AddCategory (new Category ("Category 1"));
            FinancerModel.AddCategory (new Category ("Category 2"));
            FinancerModel.AddCategory (new Category ("Category 3"));
        }

        private static void InsertDummyTransactions ()
        {
            GenerateTransaction ("fadsfasdf");
            GenerateTransaction ("ffadsfasd");
            GenerateTransaction ("safdsafas");
            GenerateTransaction ("agsdagsag");
            GenerateTransaction ("asdagsadgsaaaa");
            GenerateTransaction ("agdsagsadgaaa");
            GenerateTransaction ("gsdagsdagasgas");
            GenerateTransaction ("cscasdf");
            GenerateTransaction ("aaaa");
            GenerateTransaction ("acdhdshsdaaa");
            GenerateTransaction ("ahgfjdfgaaa");
            GenerateTransaction ("aatryrtyrtaa");
            GenerateTransaction ("aautrurjfgtyraa");
            GenerateTransaction ("aawwerraa");
            GenerateTransaction ("aaawerwqra");
            GenerateTransaction ("aaaa");
            GenerateTransaction ("aaarewqrqwa");
            GenerateTransaction ("areqrqaaa");
        }

        private static Random random = new Random ();

        private static void GenerateTransaction (string description)
        {
            var amount = random.NextDouble () * 1000;

            var categoryId = FinancerModel.GetCategories ().ElementAt (random.Next (FinancerModel.GetCategories ().Count ())).Id;

            var user1Id = FinancerModel.GetPeople ().First ().Id;
            var user2Id = FinancerModel.GetPeople().ElementAt(random.Next (1, FinancerModel.GetPeople().Count() - 1)).Id;

            int senderId;
            int receiverId;
            if (random.Next (2) == 0) {
                senderId = user1Id;
                receiverId = user2Id;
            } else {
                senderId = user2Id;
                receiverId = user1Id;
            }

            var transId = FinancerModel.AddTransaction (new Transaction () {
                CategoryId = categoryId,
                Amount = amount,
                Date = DateTime.Today.AddDays(random.Next(-5, 5)),
                Description = description,
                ReceiverId = receiverId,
            });

            FinancerModel.AddTransactionSender (new TransactionSender () {
                PersonId = senderId,
                Share = 1,
                TransactionId = transId
            });
        }
    }
}

