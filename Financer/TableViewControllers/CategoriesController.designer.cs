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
	[Register ("CategoriesController")]
	partial class CategoriesController
	{
		[Outlet]
		protected override MonoTouch.UIKit.UIBarButtonItem AddItemButton { get; set; }

		[Outlet]
		protected override MonoTouch.UIKit.UISearchBar SearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
            if (SearchBar != null) {
                SearchBar.Dispose ();
                SearchBar = null;
			}

            if (AddItemButton != null) {
                AddItemButton.Dispose ();
                AddItemButton = null;
			}
		}
	}
}
