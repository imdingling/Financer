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
	[Register ("PeopleController")]
	partial class PeopleController
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem AddPersonButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar PeopleSearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PeopleSearchBar != null) {
				PeopleSearchBar.Dispose ();
				PeopleSearchBar = null;
			}

			if (AddPersonButton != null) {
				AddPersonButton.Dispose ();
				AddPersonButton = null;
			}
		}
	}
}
