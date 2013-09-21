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
    [Register("CategoriesCell")]
	public partial class CategoriesCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel AmountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel CategoryNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CategoryNameLabel != null) {
				CategoryNameLabel.Dispose ();
				CategoryNameLabel = null;
			}

			if (AmountLabel != null) {
				AmountLabel.Dispose ();
				AmountLabel = null;
			}
		}
	}
}
