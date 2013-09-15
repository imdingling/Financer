using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class CategoriesController : UITableViewController
    {
        private Dictionary<char, Category[]> filteredCategories; 
        public Dictionary<char, Category[]> FilteredCategories { 
            get {
                return this.filteredCategories;
            }
            private set {
                if (this.filteredCategories != value) {
                    this.filteredCategories = value;
                    if (this.categoriesSource != null) {
                        this.categoriesSource.Update (value);
                    }
                }
            }
        }
        private LazyInvoker lazySearchTimer;
        private CategoriesSource categoriesSource;

        public bool IsSelecting { get; set; }

        public CategoriesController () : base ()
        {
            this.Initialize();
        }

        public CategoriesController(IntPtr handle) : base(handle)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.FilteredCategories = new Dictionary<char, Category[]> ();
            this.categoriesSource = new CategoriesSource (this.FilteredCategories);
            this.lazySearchTimer = new LazyInvoker (0.5, this.Search);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            TableView.Source = this.categoriesSource;

            this.TableView.Scrolled += this.TableViewScrolled;

            this.CategoriesSearchBar.TextChanged += this.HandleSearchBarTextChanged;
        }

        public override void ViewWillAppear (bool animated)
        {
            base.ViewWillAppear (animated);
            this.TableView.SetContentOffset (new PointF (0, 44), true);
            this.UpdateFilteredCategories ();
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "Old") {
                var controller = segue.DestinationViewController as CategoryController;
                if (controller != null) {
                    controller.Category = this.FilteredCategories.CategoryForIndexPath (this.TableView.IndexPathForSelectedRow);
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        private void TableViewScrolled (object sender, EventArgs e)
        {
            this.CategoriesSearchBar.ResignFirstResponder ();
        }

        private void HandleSearchBarTextChanged (object sender, UISearchBarTextChangedEventArgs e)
        {
            lazySearchTimer.Run ();
        }

        private void Search()
        {
            this.UpdateFilteredCategories ();
            this.TableView.ReloadData ();
        }

        private void UpdateFilteredCategories()
        {
            this.FilteredCategories = Category.GetCategoriesDictionary (FinancerModel.GetCategories().Where (category => category.ContainsSearchWord(this.CategoriesSearchBar.Text)));
            this.TableView.ReloadData ();
        }
    }
}

