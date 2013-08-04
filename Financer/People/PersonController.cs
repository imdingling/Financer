using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.AddressBook;
using MonoTouch.AddressBookUI;

namespace Financer
{
    public partial class PersonController : UIViewController
    {
        public Person Person { get; set; }

        private UIActionSheet deleteActionSheet;
        private UIActionSheet pickPhotoActionSheet;
        private TransactionsSource transactionsSource;
        private Dictionary<DateTime, Transaction[]> transactions;

        private bool isEditing;
        private bool IsEditing {
            get {
                return this.isEditing;
            }
            set {
                this.isEditing = value;
                this.NameTextField.Enabled = value;
                this.EmailTextField.Enabled = value;
                this.DeleteButton.Hidden = !value;
                this.TransactionsTableView.Hidden = value;
                this.HistoryLabel.Hidden = value;
                this.NavigationItem.HidesBackButton = value;
                this.PhotoButton.Enabled = value;

                if (value) {
                    this.NavigationItem.SetLeftBarButtonItem(this.cancelBarButton, true);
                } else {
                    this.NavigationItem.SetLeftBarButtonItem(null, true);
                }

                this.SetupBarButton ();
            }
        }

        private UIBarButtonItem cancelBarButton;

        public PersonController () : base ()
        {
        }

        public PersonController(IntPtr handle) : base (handle)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.SetupCancelButton ();
            this.SetupDeleteButton ();
            this.SetupTableView ();
            this.SetupImageView ();

            if (this.Person != null) {
                this.IsEditing = false;
                this.LoadFromPerson ();
            } else {
                this.IsEditing = true;
                this.LoadRandomImage ();
            }
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "Old") {
                var controller = segue.DestinationViewController as TransactionController;
                if (controller != null) {
                    controller.Transaction = this.transactions.TransactionForIndexPath (this.TransactionsTableView.IndexPathForSelectedRow);
                }
            }

            base.PrepareForSegue (segue, sender);
        }

        #region Setups
        private void SetupImageView()
        {
            this.PhotoButton.TouchUpInside += this.OnPhotoButtonClicked;

            this.pickPhotoActionSheet = new UIActionSheet ("Select from", null, "Cancel", null, "Camera", "Library", "Contact");
            this.pickPhotoActionSheet.Clicked += this.OnPickPhotoActionSheetClicked;
        }

        private void SetupDeleteButton()
        {
            this.DeleteButton.TouchUpInside += this.OnDeleteButtonPressed;

            this.deleteActionSheet = new UIActionSheet ("Confirm delete", null, "Cancel", "Delete");
            this.deleteActionSheet.Clicked += this.OnDeleteActionSheetClicked;
        }

        private void SetupCancelButton()
        {
            this.cancelBarButton = new UIBarButtonItem (UIBarButtonSystemItem.Cancel);
            this.cancelBarButton.Clicked += this.OnCancelButtonClicked;
        }

        private void SetupTableView()
        {
            if (this.Person == null) {
                return;
            }

            this.transactions = FinancerModel.GetTransactions ().Where (t => t.ReceiverId == this.Person.Id || t.SenderId == this.Person.Id).ToTransactionDictionary ();
            this.transactionsSource = new TransactionsSource (this.transactions);
            this.TransactionsTableView.Source = this.transactionsSource;
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
        private void OnPhotoButtonClicked (object sender, EventArgs e)
        {
            this.pickPhotoActionSheet.ShowFromTabBar (this.TabBarController.TabBar);
        }

        private void OnPickPhotoActionSheetClicked (object sender, UIButtonEventArgs e)
        {
            switch (e.ButtonIndex) {
            case 0: 
                var cameraPicker = new UIImagePickerController ();
                cameraPicker.SourceType = UIImagePickerControllerSourceType.Camera;
                cameraPicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
                cameraPicker.FinishedPickingMedia += this.OnFinishedPickingPhoto;
                cameraPicker.AllowsEditing = true;
                cameraPicker.Canceled += this.OnCanceledPickingPhoto;
                this.NavigationController.PresentViewController (cameraPicker, true, null);
                break;
            case 1:
                var libraryPicker = new UIImagePickerController ();
                libraryPicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
                libraryPicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
                libraryPicker.FinishedPickingMedia += this.OnFinishedPickingPhoto;
                libraryPicker.Canceled += this.OnCanceledPickingPhoto;
                this.NavigationController.PresentViewController(libraryPicker, true, null);
                break;
            case 2:
                var addressBookPicker = new ABPeoplePickerNavigationController ();
                addressBookPicker.Cancelled += this.OnCanceledPickingContact;
                addressBookPicker.SelectPerson += this.OnPersonSelected;
                this.NavigationController.PresentViewController (addressBookPicker, true, null);
                break;
            }
        }

        private void OnPersonSelected (object sender, ABPeoplePickerSelectPersonEventArgs e)
        {
            this.NameTextField.Text = e.Person.FirstName + " " + e.Person.LastName;
            if (e.Person.GetEmails ().Any()) {
                this.EmailTextField.Text = e.Person.GetEmails ().First ().Value;
            }

            if (e.Person.HasImage) {
                this.PhotoImageView.Image = UIImage.LoadFromData (e.Person.Image);
            }

            var picker = sender as ABPeoplePickerNavigationController;
            picker.DismissViewController (true, null);
        }

        private void OnCanceledPickingContact (object sender, EventArgs e)
        {
            var picker = sender as ABPeoplePickerNavigationController;
            picker.DismissViewController (true, null);
        }

        private void OnCanceledPickingPhoto (object sender, EventArgs e)
        {
            var picker = sender as UIImagePickerController;
            picker.DismissViewController (true, null);
        }

        private void OnFinishedPickingPhoto (object sender, UIImagePickerMediaPickedEventArgs e)
        {
            var image = e.Info[UIImagePickerController.EditedImage] as UIImage ?? e.Info [UIImagePickerController.OriginalImage] as UIImage;
            if (image != null) {
                this.PhotoImageView.Image = image.Scale (new SizeF (200, 200)).ReduceSize (0.9f);
            }

            var picker = sender as UIImagePickerController;
            picker.DismissViewController (true, null);
        }

        private void OnDeleteButtonPressed (object sender, EventArgs e)
        {
            if (this.Person == null) {
                this.NavigationController.PopViewControllerAnimated (true);
            } else {
                this.deleteActionSheet.ShowFromTabBar (this.TabBarController.TabBar);
            }
        }

        private void OnDeleteActionSheetClicked (object sender, UIButtonEventArgs e)
        {
            if (e.ButtonIndex == 0) {
                FinancerModel.Delete (this.Person);
                this.NavigationController.PopViewControllerAnimated (true);
            }
        }

        private void DoneButtonClicked (object sender, EventArgs e)
        {
            this.IsEditing = false;
            this.SaveCategory ();
            this.LoadFromPerson ();
        }

        private void EditButtonClicked (object sender, EventArgs e)
        {
            this.IsEditing = true;
        }

        private void OnCancelButtonClicked (object sender, EventArgs e)
        {
            if (this.Person == null) {
                this.NavigationController.PopViewControllerAnimated(true);
            } else {
                this.LoadFromPerson();
            }

            this.IsEditing = false;
        }
        #endregion

        private void LoadRandomImage()
        {
            this.PhotoImageView.Image = Sys.RandomPersonImage;
        }

        private void LoadFromPerson()
        {
            this.NavigationItem.Title = this.Person.Name;
            this.NameTextField.Text = this.Person.Name;
            this.EmailTextField.Text = this.Person.Email;
            this.PhotoImageView.Image = this.Person.UIImage;
        }

        private void SaveCategory()
        {
            if (this.Person == null) {
                this.Person = new Person ();
            }

            this.Person.Name = this.NameTextField.Text;
            this.Person.Email = this.EmailTextField.Text;
            if (this.PhotoImageView.Image != null) {
                this.Person.Image = this.PhotoImageView.Image.AsPNG ().ToArray ();
            }

            FinancerModel.AddOrUpdate (this.Person);
        }
    }
}

