using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using System.Collections.Generic;

namespace Financer
{
    public class CategoriesSource : UITableViewSource
    {
        private Dictionary<char, Category[]> categories;
        private string headerText;
        private Action<Category> callback;

        public CategoriesSource (Dictionary<char, Category[]> categories, string headerText = null, Action<Category> callback = null)
        {
            this.Update(categories);
            this.headerText = headerText;
            this.callback = callback;
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return categories.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.categories.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            if (!string.IsNullOrEmpty (this.headerText)) {
                return this.headerText;
            } else if (this.categories.Count < 2) {
                return null;
            } else {
                return this.categories.ElementAt (section).Key.ToString ();
            }
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

            var category = this.categories.CategoryForIndexPath(indexPath);
            cell.UpdateCell (category);

            return cell;
        }

        public void Update(Dictionary<char, Category[]> categories, UITableView tableView = null)
        {
            this.categories = categories;
            if (tableView != null) {
                tableView.ReloadData ();
            }
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow (indexPath, true);
            if (this.callback != null) {
                var category = this.categories.CategoryForIndexPath (indexPath);
                callback (category);
            }
        }
    }
}