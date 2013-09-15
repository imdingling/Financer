using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Financer
{
    public class PeopleSource : UITableViewSource
    {
        private Dictionary<char, Person[]> people;
        private string headerText;
        private Action<Person> callback;

        public PeopleSource (Dictionary<char, Person[]> people, string headerText = null, Action<Person> callback = null)
        {
            this.Update (people);
            this.headerText = headerText;
            this.callback = callback;
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return people.Keys.Count;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return this.people.ElementAt(section).Value.Length;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            if (!string.IsNullOrEmpty (this.headerText)) {
                return this.headerText;
            } else if (this.people.Count < 2) {
                return null;
            } else {
                return this.people.ElementAt (section).Key.ToString ();
            }
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

            var person = this.people.PersonForIndexPath(indexPath);
            cell.UpdateCell (person);

            return cell;
        }

        public void Update(Dictionary<char, Person[]> people, UITableView tableView = null)
        {
            this.people = people;
            if (tableView != null) {
                tableView.ReloadData ();
            }
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow (indexPath, true);
            if (this.callback != null) {
                var person = this.people.PersonForIndexPath (indexPath);
                this.callback (person);
            }
        }
    }
}