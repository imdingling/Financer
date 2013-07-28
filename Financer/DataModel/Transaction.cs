using System;
using System.Linq;
using System.Collections.Generic;
using SQLite;
using System.Threading.Tasks;

namespace Financer
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // ForeignKey
        public int ReceiverId { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        // ForeignKey
        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public Person Receiver { 
            get {
                return FinancerModel.GetPeople().FirstOrDefault (p => p.Id == this.ReceiverId);
            }
        }

        public Dictionary<Person, double> Senders
        {
            get {
                return FinancerModel.GetTransactionSenders ()
                                    .Where (ts => ts.TransactionId == this.Id)
                                    .ToDictionary (ts => FinancerModel.GetPeople ().First (p => p.Id == ts.PersonId),
                                                   ts => ts.Share);
            }
        }

        public Category Category {
            get {
                return FinancerModel.GetCategories ().FirstOrDefault (cat => cat.Id == this.CategoryId);
            }
        }

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
                this.Category.Description.Contains (value, StringComparison.OrdinalIgnoreCase) ||
                (this.IsInbound ? this.SendersString.ToString ().Contains (value, StringComparison.OrdinalIgnoreCase) : this.Receiver.ToString ().Contains (value, StringComparison.OrdinalIgnoreCase));
        }
    }
}

