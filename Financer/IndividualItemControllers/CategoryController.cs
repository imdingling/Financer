using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AdvancedColorPicker;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public partial class CategoryController : UIViewController
    {
        public Category Category { get; set; }

        private UIActionSheet deleteActionSheet;
        private TransactionsSource transactionsSource;
        private Dictionary<string, object[]> transactions;
        private Transaction selectedTransaction;

        private bool isEditing;
        private bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                this.isEditing = value;
                this.NameTextField.Enabled = value;
                this.DescriptionTextField.Enabled = value;
                this.ColorButton.Enabled = value;
                this.NavigationItem.HidesBackButton = value;
                this.ColorButton.TitleLabel.Hidden = !value;
                this.DeleteButton.Hidden = !value;
                this.HistoryLabel.Hidden = value;
                this.TransactionsTableView.Hidden = value;

                if (value) {
                    this.NavigationItem.SetLeftBarButtonItem(this.cancelBarButton, true);
                } else {
                    this.NavigationItem.SetLeftBarButtonItem(null, true);
                }

                this.SetupBarButton ();
            }
        }

        private ColorPickerViewController colorPicker;
        private UINavigationController colorPickerNavigationController;
        private UIBarButtonItem cancelBarButton;

        public CategoryController () : base ()
        {
        }

        public CategoryController(IntPtr handle) : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.ColorButton.TouchUpInside += this.OnColorButtonTouchUpInside;
            this.SetupColorPicker ();
            this.SetupCancelButton ();
            this.SetupDeleteButton ();
            this.SetupTableView ();

            if (this.Category != null) {
                this.IsEditing = false;
                this.LoadFromCategory ();
            } else {
                this.IsEditing = true;
            }
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == App.OldSegueIdentifier) {
                var controller = segue.DestinationViewController as TransactionController;
                if (controller != null) {
                    controller.Transaction = this.selectedTransaction;
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        #region Setups
        private void SetupDeleteButton()
        {
            this.DeleteButton.TouchUpInside += this.OnDeleteButtonPressed;

            this.deleteActionSheet = new UIActionSheet ("Confirm delete", null, "Cancel", "Delete");
            this.deleteActionSheet.Clicked += this.OnDeleteActionSheetClicked;
        }

        private void SetupTableView()
        {
            if (this.Category == null) {
                return;
            }

            this.transactions = FinancerModel.GetTransactions ().Where (t => t.CategoryId == this.Category.Id).ToTransactionDictionary ();
            this.transactionsSource = new TransactionsSource (this.transactions);
            this.transactionsSource.Callback = (transaction) => {
                this.selectedTransaction = transaction as Transaction;
                this.PerformSegue(App.OldSegueIdentifier, this);
            };
            this.TransactionsTableView.Source = this.transactionsSource;
        }

        private void SetupColorPicker()
        {
            this.colorPicker = new ColorPickerViewController();
            this.colorPicker.Title = "Pick a color!";
            this.colorPickerNavigationController = new UINavigationController(this.colorPicker);
            this.colorPickerNavigationController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
            this.colorPicker.NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Done);
            this.colorPicker.NavigationItem.RightBarButtonItem.Clicked += this.OnColorPickerDoneButtonClicked;
        }

        private void SetupCancelButton()
        {
            this.cancelBarButton = new UIBarButtonItem (UIBarButtonSystemItem.Cancel);
            this.cancelBarButton.Clicked += this.OnCancelButtonClicked;
        }

        private void SetupBarButton()
        {
            this.RightBarButton.Clicked -= this.DoneButtonClicked;
            this.RightBarButton.Clicked -= this.EditButtonClicked;

            if (this.IsEditing) {
                this.RightBarButton.Title = "Done";
                this.RightBarButton.Clicked += this.DoneButtonClicked;
            } else {
                this.RightBarButton.Title = "Edit";
                this.RightBarButton.Clicked += this.EditButtonClicked;
            }
        }
        #endregion

        #region Events
        private void OnDeleteButtonPressed (object sender, EventArgs e)
        {
            if (this.Category == null) {
                this.NavigationController.PopViewControllerAnimated (true);
            } else {
                this.deleteActionSheet.ShowFromTabBar (this.TabBarController.TabBar);
            }
        }

        private void OnDeleteActionSheetClicked (object sender, UIButtonEventArgs e)
        {
            if (e.ButtonIndex == 0) {
                FinancerModel.Delete (this.Category);
                this.NavigationController.PopViewControllerAnimated (true);
            }
        }

        private void OnColorButtonTouchUpInside (object sender, EventArgs e)
        {
            this.colorPicker.SelectedColor = this.ColorButton.BackgroundColor;
            this.NavigationController.PresentViewController(this.colorPickerNavigationController,true, null);
        }

        private void DoneButtonClicked (object sender, EventArgs e)
        {
            this.IsEditing = false;
            this.SaveCategory ();
            this.LoadFromCategory ();
        }

        private void EditButtonClicked (object sender, EventArgs e)
        {
            this.IsEditing = true;
        }

        private void OnCancelButtonClicked (object sender, EventArgs e)
        {
            if (this.Category == null) {
                this.NavigationController.PopViewControllerAnimated(true);
            } else {
                this.LoadFromCategory();
            }

            this.IsEditing = false;
        }

        private void OnColorPickerDoneButtonClicked (object sender, EventArgs e)
        {
            this.colorPickerNavigationController.DismissViewController(true, () => {
                this.ColorButton.BackgroundColor = this.colorPicker.SelectedColor;
            });
        }
        #endregion

        private void LoadFromCategory()
        {
            this.NavigationItem.Title = this.Category.Name;
            this.NameTextField.Text = this.Category.Name;
            this.DescriptionTextField.Text = this.Category.Description;
            this.ColorButton.BackgroundColor = this.Category.Color;
        }

        private void SaveCategory()
        {
            if (this.Category == null) {
                this.Category = new Category ();
            }

            this.Category.Name = this.NameTextField.Text;
            this.Category.Description = this.DescriptionTextField.Text;
            this.Category.Color = this.ColorButton.BackgroundColor;

            FinancerModel.AddOrUpdate (this.Category);
        }
    }
}

