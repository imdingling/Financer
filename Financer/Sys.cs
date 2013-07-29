using System;
using MonoTouch.UIKit;

namespace Financer
{
    public static class Sys
    {
        public static UIColor RedColor { 
            get {
                return UIColor.FromRGB (150, 0, 0);
            }
        }

        public static UIColor GreenColor { 
            get {
                return UIColor.FromRGB (0, 150, 0);
            }
        }
    }
}

