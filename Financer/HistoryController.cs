using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    public partial class HistoryController : UITableViewController
    {
        public HistoryController ()
        {
        }

        public HistoryController (IntPtr handle) : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            if (this.TableView != null && this.TableView.Source == null) {
                this.TableView.Source = new HistorySource (App.Transactions);
            }
        }

        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);
        }
    }
}