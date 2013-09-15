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
        private Dictionary<char, Person[]> filteredPeople;
        public Dictionary<char, Person[]> FilteredPeople { 
            get {
                return this.filteredPeople;
            }
            private set {
                if (value != this.filteredPeople) {
                    this.filteredPeople = value;
                    if (this.peopleSource != null) {
                        this.peopleSource.Update (value);
                    }
                }
            }
        }

        public bool IsSelecting { get; set; }

        private PeopleSource peopleSource;
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
            this.peopleSource = new PeopleSource (this.FilteredPeople);
            this.lazySearchTimer = new LazyInvoker (0.5, this.UpdateFilteredPeople);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            TableView.Source = this.peopleSource;

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

        private void UpdateFilteredPeople ()
        {
            this.FilteredPeople = Person.GetPeopleDictionary (FinancerModel.GetOtherPeople ().Where (person => person.ContainsSearchWord(this.PeopleSearchBar.Text)));
            this.TableView.ReloadData ();
        }
    }
}

