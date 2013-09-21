using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;

namespace Financer
{
    public abstract class TableViewSourceBase<T1, T2> : UITableViewSource
    {
        protected Dictionary<T1, T2[]> items;
        public string HeaderText { get; set; }
        public Action<T2> Callback { get; set; }

        protected TableViewSourceBase(Dictionary<T1, T2[]> items) : base()
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
                return this.GetKeyString (this.items.ElementAt (section).Key);
            }
        }

        protected virtual string GetKeyString(T1 key)
        {
            return key.ToString ();
        }

        public override string TitleForFooter (UITableView tableView, int section)
        {
            return null;
        }

        public void Update(Dictionary<T1, T2[]> items)
        {
            this.items = items;
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow (indexPath, true);
            if (this.Callback != null) {
                var item = this.items.ItemForIndexPath<T1, T2>(indexPath);
                this.Callback (item);
            }
        }
    }
}

