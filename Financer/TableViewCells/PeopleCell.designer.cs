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
    [Register("PeopleCell")]
    public partial class PeopleCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel AmountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView DirectionImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel NameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DirectionImage != null) {
				DirectionImage.Dispose ();
				DirectionImage = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (AmountLabel != null) {
				AmountLabel.Dispose ();
				AmountLabel = null;
			}
		}
	}
}
