using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace Financer
{
    public partial class TransactionsCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString ("TransactionsCell");

        public TransactionsCell () : base ()
        {
        }

        public TransactionsCell(IntPtr handle) : base(handle)
        {
        }

        public void UpdateCell(Transaction transaction)
        {
            if (transaction == null) {
                return;
            }

            this.DirectionImage.Image = GetDirectionImage(transaction);
            this.DescriptionLabel.Text = transaction.Description;
            this.AmountLabel.Text = transaction.Amount.ToString ("0.00") + " лв.";
            this.AmountLabel.TextColor = GetAmountColor (transaction);
            this.DetailsLabel.Text = GetDetailsString (transaction);
        }

        private static UIImage GetDirectionImage(Transaction transaction)
        {
            return null;
        }

        private static string GetDetailsString(Transaction transaction)
        {
            if (transaction.IsInbound) {
                return transaction.Sender.ToString();
            } else {
                return transaction.Receiver.ToString ();
            }
        }

        private static UIColor GetAmountColor(Transaction transaction)
        {
            return transaction.IsInbound ? Sys.GreenColor : Sys.RedColor;
        }
    }
}