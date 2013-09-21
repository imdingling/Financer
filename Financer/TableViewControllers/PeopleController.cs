using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Financer
{
    public partial class PeopleController : TableViewControllerBase<char, Person>
    {
        protected override void InitializeTableViewSource (Dictionary<char, Person[]> items)
        {
            this.tableViewSource = new PeopleSource (items);
        }

        public PeopleController () : base ()
        {
        }

        public PeopleController (IntPtr handle) : base(handle)
        {
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == OldSegueIdentifier) {
                var controller = segue.DestinationViewController as PersonController;
                if (controller != null) {
                    controller.Person = this.SelectedItem;
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        protected override void UpdateFilteredItems ()
        {
            this.FilteredItems = FinancerModel.GetOtherPeople ().Where (person => person.ContainsSearchWord(this.SearchBar.Text)).GetPeopleDictionary();
        }
    }
}

