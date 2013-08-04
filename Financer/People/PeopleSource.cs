using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    public class PeopleSource : UITableViewSource
    {
        private PeopleController controller;

        public PeopleSource (PeopleController controller)
        {
            this.controller = controller;
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return controller.FilteredPeople.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.controller.FilteredPeople.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            return this.controller.FilteredPeople.ElementAt (section).Key.ToString ();
        }

        public override string TitleForFooter (UITableView tableView, int section)
        {
            return null;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell (PeopleCell.Key) as PeopleCell;
            if (cell == null) {
                cell = new PeopleCell ();
            }

            var person = this.controller.FilteredPeople.PersonForIndexPath(indexPath);
            cell.UpdateCell (person);

            return cell;
        }
    }
}

