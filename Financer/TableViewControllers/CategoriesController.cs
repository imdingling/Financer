using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class CategoriesController : TableViewControllerBase<char, Category>
    {
        protected override void InitializeTableViewSource (Dictionary<char, Category[]> items)
        {
            this.tableViewSource = new CategoriesSource (items);
        }

        public CategoriesController () : base ()
        {
        }

        public CategoriesController(IntPtr handle) : base(handle)
        {
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == OldSegueIdentifier) {
                var controller = segue.DestinationViewController as CategoryController;
                if (controller != null) {
                    controller.Category = this.SelectedItem;
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        protected override void UpdateFilteredItems ()
        {
            this.FilteredItems = FinancerModel.GetCategories().Where (category => category.ContainsSearchWord(this.SearchBar.Text)).GetCategoriesDictionary();
        }
    }
}

