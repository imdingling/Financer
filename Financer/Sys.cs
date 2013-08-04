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

        private static Random random = new Random();
        public static Random Random {
            get {
                return random;
            }
        }

        public static UIImage RandomPersonImage
        {
            get {
                return UIImage.FromBundle ("Icons/Person-" + Sys.Random.Next (1, 6));
            }
        }
    }
}

