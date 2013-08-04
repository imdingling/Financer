using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Financer
{
    public partial class PeopleController : UITableViewController
    {
        public Dictionary<char, Person[]> FilteredPeople { get; private set; }

        private LazyInvoker lazySearchTimer;

        public PeopleController () : base ()
        {
            this.Initialize ();
        }

        public PeopleController (IntPtr handle) : base(handle)
        {
            this.Initialize ();
        }

        private void Initialize ()
        {
            this.FilteredPeople = new Dictionary<char, Person[]> ();
            this.lazySearchTimer = new LazyInvoker (0.5, this.Search);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            TableView.Source = new PeopleSource (this);

            this.TableView.Scrolled += this.TableViewScrolled;

            this.PeopleSearchBar.TextChanged += this.HandleSearchBarTextChanged;
        }

        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);
            this.TableView.SetContentOffset (new PointF (0, 44), true);
            this.UpdateFilteredPeople ();
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "Old") {
                var controller = segue.DestinationViewController as PersonController;
                if (controller != null) {
                    controller.Person = this.FilteredPeople.PersonForIndexPath (this.TableView.IndexPathForSelectedRow);
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        private void TableViewScrolled (object sender, EventArgs e)
        {
            this.PeopleSearchBar.ResignFirstResponder ();
        }

        private void HandleSearchBarTextChanged (object sender, UISearchBarTextChangedEventArgs e)
        {
            lazySearchTimer.Run ();
        }

        private void Search ()
        {
            this.UpdateFilteredPeople ();
            this.FilteredPeople = GetPeopleDictionary (FinancerModel.GetOtherPeople ().Where (person => person.ToString ().Contains (this.PeopleSearchBar.Text, StringComparison.OrdinalIgnoreCase)));
            this.TableView.ReloadData ();
        }

        private void UpdateFilteredPeople ()
        {
            this.FilteredPeople = GetPeopleDictionary (FinancerModel.GetOtherPeople ().Where (person => person.ContainsSearchWord(this.PeopleSearchBar.Text)));
            this.TableView.ReloadData ();
        }

        private static Dictionary<char, Person[]> GetPeopleDictionary (IEnumerable<Person> people)
        {
            return people.GroupBy (person => person.Name [0]).OrderBy(gr => gr.Key).ToDictionary (gr => gr.Key, gr => gr.ToArray ());
        }
    }
}

