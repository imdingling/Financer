using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using System.Collections;

namespace Financer
{
    public abstract class TableViewSourceBase : UITableViewSource
    {
        protected Dictionary<string, object[]> items;
        public string HeaderText { get; set; }
        public Action<object> Callback { get; set; }

        protected TableViewSourceBase(Dictionary<string, object[]> items) : base()
        {
            this.Update (items);
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return this.items.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.items.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            if (!string.IsNullOrEmpty (this.HeaderText)) {
                return this.HeaderText;
            } else if (this.items.Count < 2) {
                return null;
            } else {
                return this.items.ElementAt (section).Key;
            }
        }

        public override string TitleForFooter (UITableView tableView, int section)
        {
            return null;
        }

        public void Update(Dictionary<string, object[]> items)
        {
            this.items = items;
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow (indexPath, true);
            if (this.Callback != null) {
                var item = this.items.ItemForIndexPath<string, object>(indexPath);
                this.Callback (item);
            }
        }
    }
}

