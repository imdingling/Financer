using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    [Register ("HistoryController")]
    public class HistoryController : UITableViewController
    {
        public HistoryController () : base (UITableViewStyle.Plain)
        {
        }

        public HistoryController (IntPtr handler)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            if (this.TableView.DataSource == null) {
                this.TableView.DataSource = new HistorySource (App.Transactions);
            }
        }

        [Action ("AddNewTransaction:")]
        public void AddNewTransaction (UIBarButtonItem sender)
        {
        }
    }
}