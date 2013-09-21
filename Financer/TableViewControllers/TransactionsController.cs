using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class TransactionsController : TableViewControllerBase
    {
        protected override UIBarButtonItem AddItemButton {
            get {
                return this.AddTransactionButton;
            }
        }

        protected override UISearchBar SearchBar {
            get {
                return this.TransactionSearchBar;
            }
        }

        public TransactionsController (IntPtr handle) : base(handle)
        {
        }

        public TransactionsController () : base()
        {
        }

        protected override void InitializeTableViewSource (Dictionary<string, object[]> items)
        {
            this.tableViewSource = new TransactionsSource (items);
        }

        protected override void UpdateFilteredItems ()
        {
            this.FilteredItems = FinancerModel.GetTransactions ().Where (transaction => transaction.ContainsSearchWord (this.SearchBar.Text)).ToTransactionDictionary();
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == App.OldSegueIdentifier) {
                var controller = segue.DestinationViewController as TransactionController;
                if (controller != null) {
                    controller.Transaction = this.SelectedItem as Transaction;
                }
            }

            base.PrepareForSegue (segue, sender);
        }
    }
}