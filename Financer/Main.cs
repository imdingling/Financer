using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SQLite;

namespace Financer
{
    public class Application
    {
        static void Main (string[] args)
        {
            FinancerModel.DB.CreateTable<Person> (CreateFlags.None);
            FinancerModel.DB.CreateTable<Category> (CreateFlags.None);
            FinancerModel.DB.CreateTable<Transaction> (CreateFlags.None);

            UIApplication.Main (args, null, "AppDelegate");
        }
    }
}
