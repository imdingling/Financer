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

        // ForeignKey
        public int SenderId { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        // ForeignKey
        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public Person Receiver { 
            get {
                return FinancerModel.GetAllPeople().FirstOrDefault (p => p.Id == this.ReceiverId);
            }
        }

        public Person Sender
        {
            get {
                return FinancerModel.GetAllPeople().FirstOrDefault (p => p.Id == this.SenderId);
            }
        }

        public Category Category {
            get {
                return FinancerModel.GetCategories ().FirstOrDefault (cat => cat.Id == this.CategoryId);
            }
        }

        public bool IsInbound {
            get {
                return App.CurrentUser.Id == this.Receiver.Id;
            }
        }

        public Person Contact {
            get {
                return this.IsInbound ? this.Sender : this.Receiver;
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
                this.Category.ContainsSearchWord(value) ||
                this.Contact.ToString ().Contains (value, StringComparison.OrdinalIgnoreCase);
        }
    }
}

