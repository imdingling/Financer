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
        private HistoryController controller;

        public HistorySource (HistoryController controller)
        {
            this.controller = controller;
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return this.controller.FilteredTransactions.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.controller.FilteredTransactions.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            return this.controller.FilteredTransactions.ElementAt (section).Key.ToString ("d");
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
      
            var transaction = this.controller.FilteredTransactions.ElementAt (indexPath.Section).Value [indexPath.Row];
            cell.UpdateCell (transaction);
      
            return cell;
        }
    }
}

