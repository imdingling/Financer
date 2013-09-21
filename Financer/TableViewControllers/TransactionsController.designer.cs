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
	[Register ("TransactionsController")]
	partial class TransactionsController
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem AddTransactionButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar TransactionSearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddTransactionButton != null) {
				AddTransactionButton.Dispose ();
				AddTransactionButton = null;
			}

			if (TransactionSearchBar != null) {
				TransactionSearchBar.Dispose ();
				TransactionSearchBar = null;
			}
		}
	}
}
