using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class HistoryController : UITableViewController
    {
        public Dictionary<DateTime, Transaction[]> FilteredTransactions;
        private LazyInvoker lazySearchTimer;

        public HistoryController ()
        {
            this.Initialize();
        }

        public HistoryController (IntPtr handle) : base (handle)
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
            if (this.TableView != null && this.TableView.Source == null) {
                this.TableView.Source = new HistorySource (this);
            }

            this.TableView.Scrolled += this.TableViewScrolled;

            this.historySearchBar.TextChanged += this.HandleSearchBarTextChanged;
            if (FinancerModel.GetTransactions().Any ()) {
                this.TableView.ScrollToRow (NSIndexPath.FromItemSection (0, 0), UITableViewScrollPosition.Top, false);
            }
        }

        private void TableViewScrolled (object sender, EventArgs e)
        {
            this.historySearchBar.ResignFirstResponder ();
        }

        private void HandleSearchBarTextChanged (object sender, UISearchBarTextChangedEventArgs e)
        {
            lazySearchTimer.Run ();
        }

        private void Search()
        {
            this.FilteredTransactions = GetTransactionDictionary (FinancerModel.GetTransactions().Where (transaction => transaction.ContainsSearchWord (this.historySearchBar.Text)));
            this.TableView.ReloadData ();
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);
        }

        private static Dictionary<DateTime, Transaction[]> GetTransactionDictionary(IEnumerable<Transaction> transactions)
        {
            return transactions.GroupBy (transaction => transaction.Date.Date).ToDictionary (gr => gr.Key, gr => gr.ToArray());
        }
    }
}