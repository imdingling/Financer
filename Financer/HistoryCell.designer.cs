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
  [Register("HistoryCell")]
	partial class HistoryCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel AmountLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DescriptionLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DetailsLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView DirectionImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DirectionImage != null) {
				DirectionImage.Dispose ();
				DirectionImage = null;
			}

			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}

			if (DetailsLabel != null) {
				DetailsLabel.Dispose ();
				DetailsLabel = null;
			}

			if (AmountLabel != null) {
				AmountLabel.Dispose ();
				AmountLabel = null;
			}
		}
	}
}
