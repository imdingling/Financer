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
	[Register ("CategoryController")]
	partial class CategoryController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton ColorButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DeleteButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField DescriptionTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel HistoryLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem RightBarButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView TransactionsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ColorButton != null) {
				ColorButton.Dispose ();
				ColorButton = null;
			}

			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}

			if (DescriptionTextField != null) {
				DescriptionTextField.Dispose ();
				DescriptionTextField = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (RightBarButton != null) {
				RightBarButton.Dispose ();
				RightBarButton = null;
			}

			if (HistoryLabel != null) {
				HistoryLabel.Dispose ();
				HistoryLabel = null;
			}

			if (TransactionsTableView != null) {
				TransactionsTableView.Dispose ();
				TransactionsTableView = null;
			}
		}
	}
}
