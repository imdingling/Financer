using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public class TransactionsSource : UITableViewSource
    {
        private Dictionary<DateTime, Transaction[]> transactions;

        public TransactionsSource (Dictionary<DateTime, Transaction[]> transactions)
        {
            this.Update (transactions);
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return this.transactions.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.transactions.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            return this.transactions.ElementAt (section).Key.ToString ("d");
        }

        public override string TitleForFooter (UITableView tableView, int section)
        {
            return null;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (TransactionsCell.Key) as TransactionsCell;
            if (cell == null) {
                cell = new TransactionsCell ();
            }
      
            var transaction = this.transactions.TransactionForIndexPath (indexPath);
            cell.UpdateCell (transaction);
      
            return cell;
        }

        public void Update(Dictionary<DateTime, Transaction[]> transactions)
        {
            this.transactions = transactions;
        }
    }
}

