using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    public partial class CategoriesCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString ("CategoriesCell");

        public CategoriesCell () : base ()
        {
        }

        public CategoriesCell(IntPtr handle) : base(handle)
        {
        }

        public void UpdateCell(Category category)
        {
            if (category == null) {
                return;
            }

            var balance = FinancerModel.GetBalance (category);

            this.CategoryNameLabel.Text = category.Name;
            this.AmountLabel.Text = balance.ToString ("0.00") + " лв.";
            this.AmountLabel.TextColor = GetAmountColor (balance);
        }

        private static UIColor GetAmountColor(double balance)
        {
            return balance > 0 ? Sys.GreenColor : Sys.RedColor;
        }
    }
}