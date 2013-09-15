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
        private const string OldSegueIdentifier = "Old";

        private Dictionary<char, Person[]> filteredPeople;
        public Dictionary<char, Person[]> FilteredPeople { 
            get {
                return this.filteredPeople;
            }
            private set {
                if (value != this.filteredPeople) {
                    this.filteredPeople = value;
                    if (this.peopleSource != null) {
                        this.peopleSource.Update (value, this.TableView);
                    }
                }
            }
        }

        private PeopleSource peopleSource;
        private LazyInvoker lazySearchTimer;

        private Action<Person> selectionCallback;
        public Action<Person> SelectionCallback { 
            get {
                return this.selectionCallback;
            }
            set {
                if (value != this.selectionCallback) {
                    this.selectionCallback = value;
                    this.AddPersonButton.Enabled = value == null;
                }
            }
        }

        private Person selectedPerson;
        private Person SelectedPerson { 
            get {
                return this.selectedPerson;
            }
            set {
                if (value != this.selectedPerson) {
                    this.selectedPerson = value;
                    if (this.SelectionCallback != null) {
                        this.SelectionCallback (value);
                    } else {
                        this.PerformSegue (OldSegueIdentifier, this);
                    }
                }
            }
        }

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
            this.peopleSource = new PeopleSource (this.FilteredPeople, null, (person) => {
                this.SelectedPerson = person;
            });
            this.lazySearchTimer = new LazyInvoker (0.5, this.UpdateFilteredPeople);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            TableView.Source = this.peopleSource;

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
            if (segue.Identifier == OldSegueIdentifier) {
                var controller = segue.DestinationViewController as PersonController;
                if (controller != null) {
                    controller.Person = this.SelectedPerson;
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        private void HandleSearchBarTextChanged (object sender, UISearchBarTextChangedEventArgs e)
        {
            lazySearchTimer.Run ();
        }

        private void UpdateFilteredPeople ()
        {
            this.FilteredPeople = FinancerModel.GetOtherPeople ().Where (person => person.ContainsSearchWord(this.PeopleSearchBar.Text)).GetPeopleDictionary();
            this.TableView.ReloadData ();
        }
    }
}

