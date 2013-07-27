using System;
using System.Linq;
using System.Collections.Generic;

namespace Financer
{
    public static class App
    {
        public static void Initialize ()
        {
            CurrentUser = new Person("Current", "Currentov");
            Transactions = GetDummyTransactions ();
        }

        public static Person CurrentUser { get; private set; }

        public static IEnumerable<Transaction> Transactions { get; private set; }

        private static IEnumerable<Transaction> GetDummyTransactions ()
        {
            return new Transaction[] {
                GenerateTransaction("fadsfasdf"),
                GenerateTransaction("ffadsfasd"),
                GenerateTransaction("safdsafas"),
                GenerateTransaction("agsdagsag"),
                GenerateTransaction("asdagsadgsaaaa"),
                GenerateTransaction("agdsagsadgaaa"),
                GenerateTransaction("gsdagsdagasgas"),
                GenerateTransaction("cscasdf"),
                GenerateTransaction("aaaa"),
                GenerateTransaction("acdhdshsdaaa"),
                GenerateTransaction("ahgfjdfgaaa"),
                GenerateTransaction("aatryrtyrtaa"),
                GenerateTransaction("aautrurjfgtyraa"),
                GenerateTransaction("aawwerraa"),
                GenerateTransaction("aaawerwqra"),
                GenerateTransaction("aaaa"),
                GenerateTransaction("aaarewqrqwa"),
                GenerateTransaction("areqrqaaa"),
            }.OrderByDescending (transaction => transaction.Date)
             .ToArray ();
        }

        private static Person[] People = new Person[] {
            new Person("Pesho", "Peshev"), 
            new Person("Niki", "Nikov"), 
            new Person("Penka", "Penkova"), 
            new Person("Tosho", "Toshev"), 
        };
        private static Random random = new Random ();

        private static Transaction GenerateTransaction (string description)
        {
            var amount = random.NextDouble () * 1000;

            var user2 = People [random.Next (People.Length)];

            Person sender;
            Person receiver;
            if (random.Next (2) == 0) {
                sender = CurrentUser;
                receiver = user2;
            } else {
                sender = user2;
                receiver = CurrentUser;
            }

            return new Transaction () {
                Reason = new TransactionType("Sample transaction"),
                Amount = amount,
                Date = DateTime.Today.AddDays(random.Next(-10, 10)),
                Description = description,
                Receiver = receiver,
                Senders = new Dictionary<Person, double>() { { sender, 1 } }
            };
        }
    }
}

