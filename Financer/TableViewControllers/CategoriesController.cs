using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class CategoriesController : TableViewControllerBase
    {
        protected override UIBarButtonItem AddItemButton {
            get {
                return this.AddCategoryButton;               
            }
        }

        protected override UISearchBar SearchBar {
            get {
                return this.CategorySearchBar;
            }
        }

        protected override void InitializeTableViewSource (Dictionary<string, object[]> items)
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
            if (segue.Identifier == App.OldSegueIdentifier) {
                var controller = segue.DestinationViewController as CategoryController;
                if (controller != null) {
                    controller.Category = this.SelectedItem as Category;
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

