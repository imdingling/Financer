using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace Financer
{
    public partial class TransactionController : UIViewController
    {
        private const string ReviewCategorySegue = "ReviewCategory";
        private const string SelectCategorySegue = "SelectCategory";
        private const string ReviewPersonSegue = "ReviewPerson";
        private const string SelectPersonSegue = "SelectPerson";

        public Transaction Transaction { get; set; }

        private UIActionSheet deleteActionSheet;
        private CategoriesSource categorySource;
        private PeopleSource peopleSource;

        private Category currentCategory;
        private Category CurrentCategory {
            get {
                return this.currentCategory;
            }
            set {
                if (value != this.currentCategory) {
                    this.currentCategory = value;
                    if (this.categorySource != null) {
                        this.categorySource.Update (new[] { this.currentCategory }.GetCategoriesDictionary(), this.CategoryTableView);
                    }
                }
            }
        }

        private Person currentPerson;
        private Person CurrentPerson {
            get {
                return this.currentPerson;
            }
            set {
                if (value != this.currentPerson) {
                    this.currentPerson = value;
                    if (this.peopleSource != null) {
                        this.peopleSource.Update (new[] { this.currentPerson }.GetPeopleDictionary(), this.PersonTableView);
                    }
                }
            }
        }

        private bool isEditing;
        private bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                this.isEditing = value;
                this.DescriptionTextField.Enabled = value;
                this.AmountTextField.Enabled = value;
                this.NavigationItem.HidesBackButton = value;
                this.DirectionSwitch.Enabled = value;
                this.DeleteButton.Hidden = !value;
                this.DateButton.Enabled = value;

                if (value) {
                    this.NavigationItem.SetLeftBarButtonItem(this.cancelBarButton, true);
                } else {
                    this.NavigationItem.SetLeftBarButtonItem(null, true);
                }

                this.SetupBarButton ();
            }
        }

        private UIBarButtonItem cancelBarButton;

        public TransactionController () : base ()
        {
        }

        public TransactionController (IntPtr handle) : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.SetupReturnKey ();
            this.SetupDeleteButton ();
            this.SetupTableViews ();
            this.SetupCancelButton ();
            this.SetupAmountTextField ();
            this.SetupDateButton ();

            if (this.Transaction != null) {
                this.IsEditing = false;
                this.LoadFromTransaction ();
            } else {
                this.IsEditing = true;
            }
        }

        #region Setups
        private delegate bool ReturnKeyDelegate (UITextField textField);

        private void SetupReturnKey()
        {
            UITextFieldCondition returnKeyDelegate = delegate(UITextField textField) {
                textField.ResignFirstResponder();
                return true;
            };

            this.DescriptionTextField.ShouldReturn += returnKeyDelegate;
            this.AmountTextField.ShouldReturn += returnKeyDelegate;
        }

        private void SetupDeleteButton()
        {
            this.DeleteButton.TouchUpInside += this.OnDeleteButtonPressed;

            this.deleteActionSheet = new UIActionSheet ("Confirm delete", null, "Cancel", "Delete");
            this.deleteActionSheet.Clicked += this.OnDeleteActionSheetClicked;
        }

        private void SetupTableViews()
        {
            this.currentCategory = this.Transaction == null ? FinancerModel.GetCategories ().FirstOrDefault () : this.Transaction.Category;
            this.categorySource = new CategoriesSource (new[] { this.currentCategory }.GetCategoriesDictionary(), "Category", (c) => {
                this.PerformSegue(this.IsEditing ? SelectCategorySegue : ReviewCategorySegue, this);
            });
            this.CategoryTableView.Source = this.categorySource;
            this.CategoryTableView.AlwaysBounceVertical = false;

            this.currentPerson = this.Transaction == null ? FinancerModel.GetOtherPeople ().FirstOrDefault () : this.Transaction.Contact;
            this.peopleSource = new PeopleSource (new[] { this.currentPerson }.GetPeopleDictionary(), "Contact", (p) => {
                this.PerformSegue(this.IsEditing ? SelectPersonSegue : ReviewPersonSegue, this);
            });
            this.PersonTableView.Source = this.peopleSource;
            this.PersonTableView.AlwaysBounceVertical = false;
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

        private void SetupAmountTextField()
        {
            this.AmountTextField.EditingChanged += this.OnAmountTextFieldEditingChanged;
        }

        private void SetupDateButton()
        {
            this.DateButton.SetTitle(DateTime.Now.ToString("g"), UIControlState.Normal);
        }
        #endregion

        #region Events
        private void OnAmountTextFieldEditingChanged (object sender, EventArgs e)
        {
            this.AmountTextField.Text = this.AmountTextField.Text.Trim ();

            double amount;
            while (!double.TryParse(this.AmountTextField.Text, out amount) && !string.IsNullOrEmpty(this.AmountTextField.Text)) {
                 this.AmountTextField.Text = this.AmountTextField.Text.Substring (0, this.AmountTextField.Text.Length - 1);
            }
        }

        private void OnDeleteButtonPressed (object sender, EventArgs e)
        {
            if (this.Transaction == null) {
                this.NavigationController.PopViewControllerAnimated (true);
            } else {
                this.deleteActionSheet.ShowFromTabBar (this.TabBarController.TabBar);
            }
        }

        private void OnDeleteActionSheetClicked (object sender, UIButtonEventArgs e)
        {
            if (e.ButtonIndex == 0) {
                FinancerModel.Delete (this.Transaction);
                this.NavigationController.PopViewControllerAnimated (true);
            }
        }

        private void DoneButtonClicked (object sender, EventArgs e)
        {
            this.IsEditing = false;
            this.SaveTransaction ();
            this.LoadFromTransaction ();
        }

        private void EditButtonClicked (object sender, EventArgs e)
        {
            this.IsEditing = true;
        }

        private void OnCancelButtonClicked (object sender, EventArgs e)
        {
            if (this.Transaction == null) {
                this.NavigationController.PopViewControllerAnimated(true);
            } else {
                this.LoadFromTransaction();
            }

            this.IsEditing = false;
        }
        #endregion

        private void LoadFromTransaction()
        {
            this.NavigationItem.Title = this.Transaction.Contact.Name;
            this.DescriptionTextField.Text = this.Transaction.Description;
            this.DirectionSwitch.On = this.Transaction.IsInbound;
            this.AmountTextField.Text = this.Transaction.Amount.ToString("0.00");
            this.DateButton.SetTitle (this.Transaction.Date.ToString ("g"), UIControlState.Normal);
        }

        private void SaveTransaction()
        {
            if (this.Transaction == null) {
                this.Transaction = new Transaction ();
            }

            double amount;
            this.Transaction.Amount = double.TryParse(this.AmountTextField.Text, out amount) ? amount : 0;
            this.Transaction.Description = this.DescriptionTextField.Text;
            this.Transaction.Date = DateTime.ParseExact(this.DateButton.Title(UIControlState.Normal), "g", null);
            this.Transaction.CategoryId = this.CurrentCategory.Id;
            this.Transaction.ReceiverId = this.DirectionSwitch.On ? App.CurrentUser.Id : this.CurrentPerson.Id;
            this.Transaction.SenderId = this.DirectionSwitch.On ? this.CurrentPerson.Id : App.CurrentUser.Id;

            FinancerModel.AddOrUpdate (this.Transaction);
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            switch (segue.Identifier) {
            case ReviewCategorySegue:
                var categoryController = segue.DestinationViewController as CategoryController;
                if (categoryController != null) {
                    categoryController.Category = this.Transaction.Category;
                }
                break;
            case SelectCategorySegue: 
                var categoriesController = segue.DestinationViewController as CategoriesController;
                if (categoriesController != null) {
                    categoriesController.SelectionCallback = (category) => {
                        categoriesController.NavigationController.PopViewControllerAnimated(true);
                        this.CurrentCategory = category;
                    };
                }
                break;
            case ReviewPersonSegue: 
                var personController = segue.DestinationViewController as PersonController;
                if (personController != null) {
                    personController.Person = this.Transaction.Contact;
                }
                break;
            case SelectPersonSegue: 
                var peopleController = segue.DestinationViewController as PeopleController;
                if (peopleController != null) {
                    peopleController.SelectionCallback = (person) => {
                        peopleController.NavigationController.PopViewControllerAnimated(true);
                        this.CurrentPerson = person;
                    };
                }
                break;
            }

            base.PrepareForSegue (segue, sender);
        }
    }
}

