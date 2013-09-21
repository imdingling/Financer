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
		MonoTouch.UIKit.UIBarButtonItem AddCategoryButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISearchBar CategorySearchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddCategoryButton != null) {
				AddCategoryButton.Dispose ();
				AddCategoryButton = null;
			}

			if (CategorySearchBar != null) {
				CategorySearchBar.Dispose ();
				CategorySearchBar = null;
			}
		}
	}
}
