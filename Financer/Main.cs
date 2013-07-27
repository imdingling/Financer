using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Financer
{
    public class Application
    {
        static void Main (string[] args)
        {
            App.Initialize ();
            UIApplication.Main (args, null, "AppDelegate");
        }
    }
}
