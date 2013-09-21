using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using System.Collections.Generic;

namespace Financer
{
    public class CategoriesSource : TableViewSourceBase<char, Category>
    {
        public CategoriesSource (Dictionary<char, Category[]> items) : base (items)
        {
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (CategoriesCell.Key) as CategoriesCell;
            if (cell == null) {
                cell = new CategoriesCell ();
            }

            var category = this.items.ItemForIndexPath(indexPath);
            cell.UpdateCell (category);

            return cell;
        }
    }
}