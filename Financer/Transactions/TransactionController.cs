using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    public partial class TransactionController : UIViewController
    {
        public Transaction Transaction { get; set; }

        public TransactionController () : base ()
        {
        }

        public TransactionController (IntPtr handle) : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
        }
    }
}

