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
        public Dictionary<DateTime, Transaction[]> FilteredTransactions { get; private set; }
        private LazyInvoker lazySearchTimer;

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
            this.FilteredTransactions = GetTransactionDictionary (FinancerModel.GetTransactions());
            this.lazySearchTimer = new LazyInvoker (0.5, this.Search);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            this.TableView.Source = new TransactionsSource (this);

            this.TableView.Scrolled += this.TableViewScrolled;

            this.HistorySearchBar.TextChanged += this.HandleSearchBarTextChanged;
            if (FinancerModel.GetTransactions().Any ()) {
                this.TableView.ScrollToRow (NSIndexPath.FromItemSection (0, 0), UITableViewScrollPosition.Top, false);
            }
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
            this.TableView.ReloadData ();
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);
        }

        private void UpdateFilteredTransactions()
        {
            this.FilteredTransactions = GetTransactionDictionary (FinancerModel.GetTransactions().Where (transaction => transaction.ContainsSearchWord (this.HistorySearchBar.Text)));
            this.TableView.ReloadData ();
        }

        private static Dictionary<DateTime, Transaction[]> GetTransactionDictionary(IEnumerable<Transaction> transactions)
        {
            return transactions.GroupBy (transaction => transaction.Date.Date).OrderByDescending(gr => gr.Key).ToDictionary (gr => gr.Key, gr => gr.ToArray());
        }
    }
}