using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public class HistorySource : UITableViewSource
    {
        private Dictionary<DateTime, Transaction[]> transactions;

        public HistorySource (IEnumerable<Transaction> transactions)
        {
            this.transactions = transactions.GroupBy (transaction => transaction.Date.Date).ToDictionary (gr => gr.Key, gr => gr.ToArray());
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return transactions.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return transactions.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            return transactions.ElementAt (section).Key.ToString ("d");
        }

        public override string TitleForFooter (UITableView tableView, int section)
        {
            return null;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (HistoryCell.Key) as HistoryCell;
            if (cell == null) {
                cell = new HistoryCell ();
            }
      
            var transaction = transactions.ElementAt (indexPath.Section).Value [indexPath.Row];
            cell.UpdateCell (transaction);
      
            return cell;
        }
    }
}

