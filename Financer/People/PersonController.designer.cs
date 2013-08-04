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
	[Register ("PersonController")]
	partial class PersonController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton DeleteButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField EmailTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel HistoryLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton PhotoButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView PhotoImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem RightBarButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView TransactionsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}

			if (EmailTextField != null) {
				EmailTextField.Dispose ();
				EmailTextField = null;
			}

			if (HistoryLabel != null) {
				HistoryLabel.Dispose ();
				HistoryLabel = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (PhotoImageView != null) {
				PhotoImageView.Dispose ();
				PhotoImageView = null;
			}

			if (RightBarButton != null) {
				RightBarButton.Dispose ();
				RightBarButton = null;
			}

			if (TransactionsTableView != null) {
				TransactionsTableView.Dispose ();
				TransactionsTableView = null;
			}

			if (PhotoButton != null) {
				PhotoButton.Dispose ();
				PhotoButton = null;
			}
		}
	}
}
