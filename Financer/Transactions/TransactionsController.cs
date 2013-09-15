using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class TransactionsController : UITableViewController
    {
        private LazyInvoker lazySearchTimer;
        private TransactionsSource transactionsSource;

        private Dictionary<DateTime, Transaction[]> filteredTransactions;
        public Dictionary<DateTime, Transaction[]> FilteredTransactions { 
            get {
                return this.filteredTransactions;
            }
            private set {
                if (value != this.filteredTransactions) {
                    this.filteredTransactions = value;
                    if (this.transactionsSource != null) {
                        this.transactionsSource.Update (value);
                    }
                }
            }
        }

        public TransactionsController ()
        {
            this.Initialize();
        }

        public TransactionsController (IntPtr handle) : base (handle)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.FilteredTransactions = new Dictionary<DateTime, Transaction[]> ();
            this.transactionsSource = new TransactionsSource (this.FilteredTransactions);
            this.lazySearchTimer = new LazyInvoker (0.5, this.Search);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            this.TableView.Source = this.transactionsSource;

            this.TableView.Scrolled += this.TableViewScrolled;

            this.HistorySearchBar.TextChanged += this.HandleSearchBarTextChanged;
//            if (FinancerModel.GetTransactions().Any ()) {
//                this.TableView.ScrollToRow (NSIndexPath.FromItemSection (0, 0), UITableViewScrollPosition.Top, false);
//            }
        }

        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);
            this.TableView.SetContentOffset (new PointF (0, 44), true);
            this.UpdateFilteredTransactions ();
        }

        private void TableViewScrolled (object sender, EventArgs e)
        {
            this.HistorySearchBar.ResignFirstResponder ();
        }

        private void HandleSearchBarTextChanged (object sender, UISearchBarTextChangedEventArgs e)
        {
            lazySearchTimer.Run ();
        }

        private void Search()
        {
            this.UpdateFilteredTransactions ();
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);
        }

        private void UpdateFilteredTransactions()
        {
            this.FilteredTransactions = FinancerModel.GetTransactions ().Where (transaction => transaction.ContainsSearchWord (this.HistorySearchBar.Text)).ToTransactionDictionary();
            this.TableView.ReloadData ();
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "Old") {
                var controller = segue.DestinationViewController as TransactionController;
                if (controller != null) {
                    controller.Transaction = this.FilteredTransactions.TransactionForIndexPath (this.TableView.IndexPathForSelectedRow);
                }
            }

            base.PrepareForSegue (segue, sender);
        }
    }
}