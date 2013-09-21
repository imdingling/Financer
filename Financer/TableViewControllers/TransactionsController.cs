using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class TransactionsController : TableViewControllerBase<DateTime, Transaction>
    {
        public TransactionsController (IntPtr handle) : base(handle)
        {
        }

        public TransactionsController () : base()
        {
        }

        protected override void InitializeTableViewSource (Dictionary<DateTime, Transaction[]> items)
        {
            this.tableViewSource = new TransactionsSource (items);
        }

        protected override void UpdateFilteredItems ()
        {
            this.FilteredItems = FinancerModel.GetTransactions ().Where (transaction => transaction.ContainsSearchWord (this.SearchBar.Text)).ToTransactionDictionary();
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "Old") {
                var controller = segue.DestinationViewController as TransactionController;
                if (controller != null) {
                    controller.Transaction = this.SelectedItem;
                }
            }

            base.PrepareForSegue (segue, sender);
        }
    }
}