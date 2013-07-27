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

        public HistoryCell () : base (UITableViewCellStyle.Default, Key)
        {
        }

        public void PopulateCell(Transaction transaction)
        {
            if (transaction == null) {
                return;
            }

            this.DirectionImage.Image = GetDirectionImage(transaction);
            this.DescriptionLabel.Text = transaction.Description;
            this.AmountLabel.Text = transaction.Amount.ToString ("C");
            this.DetailsLabel.Text = transaction.Receiver.ToString ();
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
    }
}