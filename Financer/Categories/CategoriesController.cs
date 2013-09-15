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
        private const string OldSegueIdentifier = "Old";

        private Dictionary<char, Category[]> filteredCategories; 
        public Dictionary<char, Category[]> FilteredCategories { 
            get {
                return this.filteredCategories;
            }
            private set {
                if (this.filteredCategories != value) {
                    this.filteredCategories = value;
                    if (this.categoriesSource != null) {
                        this.categoriesSource.Update (value, this.TableView);
                    }
                }
            }
        }
        private LazyInvoker lazySearchTimer;
        private CategoriesSource categoriesSource;

        private Action<Category> selectionCallback;
        public Action<Category> SelectionCallback { 
            get {
                return this.selectionCallback;
            }
            set {
                if (value != this.selectionCallback) {
                    this.selectionCallback = value;
                    this.AddCategoryButton.Enabled = value == null;
                }
            }
        }

        private Category selectedCategory;
        private Category SelectedCategory { 
            get {
                return this.selectedCategory;
            }
            set {
                if (value != this.selectedCategory) {
                    this.selectedCategory = value;
                    if (this.SelectionCallback != null) {
                        this.SelectionCallback (value);
                    } else {
                        this.PerformSegue (OldSegueIdentifier, this);
                    }
                }
            }
        }

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
            this.categoriesSource = new CategoriesSource (this.FilteredCategories, null, (category) => {
                this.SelectedCategory = category;
            });
            this.lazySearchTimer = new LazyInvoker (0.5, this.UpdateFilteredCategories);
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            this.TableView.Source = this.categoriesSource;

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
            if (segue.Identifier == OldSegueIdentifier) {
                var controller = segue.DestinationViewController as CategoryController;
                if (controller != null) {
                    controller.Category = this.SelectedCategory;
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        private void HandleSearchBarTextChanged (object sender, UISearchBarTextChangedEventArgs e)
        {
            lazySearchTimer.Run ();
        }

        private void UpdateFilteredCategories()
        {
            this.FilteredCategories = FinancerModel.GetCategories().Where (category => category.ContainsSearchWord(this.CategoriesSearchBar.Text)).GetCategoriesDictionary();
            this.TableView.ReloadData ();
        }
    }
}

