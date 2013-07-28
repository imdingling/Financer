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

        public bool ContainsSearchWord(string value)
        {
            if (string.IsNullOrEmpty (value)) {
                return true;
            }

            var firstChar = value [0];
            if (firstChar.In('<', '>', '=')) {
                double doubleValue;
                if (double.TryParse (value.Substring (1), out doubleValue)) {
                    switch (firstChar) {
                        case '<': return this.Amount < doubleValue;
                        case '>': return this.Amount > doubleValue;
                        case '=': return Math.Abs(this.Amount - doubleValue) < 0.5;
                    }
                } else {
                    return value.Length == 1;
                }
            }

            return this.Description.Contains (value, StringComparison.OrdinalIgnoreCase) ||
                this.Reason.Description.Contains (value, StringComparison.OrdinalIgnoreCase) ||
                (this.IsInbound ? this.SendersString.ToString ().Contains (value, StringComparison.OrdinalIgnoreCase) : this.Receiver.ToString ().Contains (value, StringComparison.OrdinalIgnoreCase));
        }
    }
}

