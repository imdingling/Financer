using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace Financer
{
    public class CategoriesSource : UITableViewSource
    {
        private CategoriesController controller;

        public CategoriesSource (CategoriesController controller)
        {
            this.controller = controller;
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return controller.FilteredCategories.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.controller.FilteredCategories.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            return this.controller.FilteredCategories.ElementAt (section).Key.ToString ();
        }

        public override string TitleForFooter (UITableView tableView, int section)
        {
            return null;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (CategoriesCell.Key) as CategoriesCell;
            if (cell == null) {
                cell = new CategoriesCell ();
            }

            var category = this.controller.FilteredCategories.CategoryForIndexPath(indexPath);
            cell.UpdateCell (category);

            return cell;
        }
    }
}

