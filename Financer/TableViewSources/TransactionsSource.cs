using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public class TransactionsSource : TableViewSourceBase
    {
        public TransactionsSource (Dictionary<string, object[]> items) : base(items)
        {
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (TransactionsCell.Key) as TransactionsCell;
            if (cell == null) {
                cell = new TransactionsCell ();
            }
      
            var transaction = this.items.ItemForIndexPath (indexPath);
            cell.UpdateCell (transaction as Transaction);
      
            return cell;
        }
    }
}

