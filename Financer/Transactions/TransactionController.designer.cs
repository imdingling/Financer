// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Financer
{
	[Register ("TransactionController")]
	partial class TransactionController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField AmountTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView CategoryTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DateButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DeleteButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField DescriptionTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch DirectionSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView PersonTableView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem RightBarButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AmountTextField != null) {
				AmountTextField.Dispose ();
				AmountTextField = null;
			}

			if (CategoryTableView != null) {
				CategoryTableView.Dispose ();
				CategoryTableView = null;
			}

			if (DateButton != null) {
				DateButton.Dispose ();
				DateButton = null;
			}

			if (DescriptionTextField != null) {
				DescriptionTextField.Dispose ();
				DescriptionTextField = null;
			}

			if (DirectionSwitch != null) {
				DirectionSwitch.Dispose ();
				DirectionSwitch = null;
			}

			if (PersonTableView != null) {
				PersonTableView.Dispose ();
				PersonTableView = null;
			}

			if (RightBarButton != null) {
				RightBarButton.Dispose ();
				RightBarButton = null;
			}

			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}
		}
	}
}
