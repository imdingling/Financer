using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SQLite;
using System.IO;

namespace Financer
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {
            FinancerModel.DB.CreateTable<Person> (CreateFlags.None);
            FinancerModel.DB.CreateTable<Category> (CreateFlags.None);
            FinancerModel.DB.CreateTable<Transaction> (CreateFlags.None);
            FinancerModel.DB.CreateTable<TransactionSender> (CreateFlags.None);

            App.Initialize ();
            return true;
        }
    }
}

