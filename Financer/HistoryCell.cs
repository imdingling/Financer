using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace Financer
{
    public partial class HistoryCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString ("HistoryCell");

        public HistoryCell () : base ()
        {
        }

        public HistoryCell(IntPtr handle) : base(handle)
        {
        }

        public void UpdateCell(Transaction transaction)
        {
            if (transaction == null) {
                return;
            }

            this.DirectionImage.Image = GetDirectionImage(transaction);
            this.DescriptionLabel.Text = transaction.Description;
            this.AmountLabel.Text = transaction.Amount.ToString ("C");
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
                return transaction.SendersString;
            } else {
                return transaction.Receiver.ToString ();
            }
        }

        private static UIColor GetAmountColor(Transaction transaction)
        {
            return transaction.IsInbound ? UIColor.Green : UIColor.Red;
        }
    }
}