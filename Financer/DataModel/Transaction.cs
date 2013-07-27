using System;
using System.Linq;
using System.Collections.Generic;

namespace Financer
{
    public class Transaction
    {
        public Dictionary<Person, double> Senders { get; set; }

        public Person Receiver { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public TransactionType Reason { get; set; }

        public DateTime Date { get; set; }

        public string SendersString {
            get {
                return string.Join (",", this.Senders.Keys.Select (person => person.ToString ()));
            }
        }

        public bool IsInbound {
            get {
                return App.CurrentUser.Equals (this.Receiver);
            }
        }
    }
}

