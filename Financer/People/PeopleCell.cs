using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    public partial class PeopleCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString ("PeopleCell");

        public PeopleCell () : base ()
        {
        }

        public PeopleCell(IntPtr handle) : base(handle)
        {
        }

        public void UpdateCell(Person person)
        {
            if (person == null) {
                return;
            }

            var balance = FinancerModel.GetBalance (person);

            this.DirectionImage.Image = person.UIImage;
            this.NameLabel.Text = person.ToString ();
            this.AmountLabel.Text = balance.ToString ("0.00") + " лв.";
            this.AmountLabel.TextColor = GetAmountColor (balance);
        }

        private static UIColor GetAmountColor(double balance)
        {
            return balance > 0 ? Sys.GreenColor : Sys.RedColor;
        }
    }
}

