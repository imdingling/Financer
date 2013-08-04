using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.IO;

namespace Financer
{
    public static class ExtensionMethods
    {
        #region string
        public static bool Contains (this string source, string value, StringComparison comparison)
        {
            return source.IndexOf (value, comparison) >= 0;
        }
        #endregion
        #region T
        public static bool In<T> (this T value, IEnumerable<T> collection)
        {
            return collection.Contains (value);
        }

        public static bool In<T> (this T value, params T[] values)
        {
            return values.Contains (value);
        }
        #endregion
        #region Dictionary<char, *[]>
        public static Category CategoryForIndexPath (this Dictionary<char, Category[]> dictionary, NSIndexPath indexPath)
        {
            return dictionary.ElementAt (indexPath.Section).Value [indexPath.Row];
        }

        public static Person PersonForIndexPath (this Dictionary<char, Person[]> dictionary, NSIndexPath indexPath)
        {
            return dictionary.ElementAt (indexPath.Section).Value [indexPath.Row];
        }

        public static Transaction TransactionForIndexPath (this Dictionary<DateTime, Transaction[]> dictionary, NSIndexPath indexPath)
        {
            return dictionary.ElementAt (indexPath.Section).Value [indexPath.Row];
        }
        #endregion
        #region IEnumerable<*>
        public static Dictionary<DateTime, Transaction[]> ToTransactionDictionary (this IEnumerable<Transaction> transactions)
        {
            return transactions.GroupBy (transaction => transaction.Date.Date).OrderByDescending (gr => gr.Key).ToDictionary (gr => gr.Key, gr => gr.ToArray ());
        }
        #endregion
        #region int
        public static UIColor ToColor (this int colorInt)
        {
            var a = (float)(colorInt & 0xFF) / 255f;
            var b = (float)(colorInt >> 8 & 0xFF) / 255f;
            var g = (float)(colorInt >> 16 & 0xFF) / 255f;
            var r = (float)(colorInt >> 24 & 0xFF) / 255f;
            return new UIColor (r, g, b, a);
        }
        #endregion
        #region UIColor
        public static int ToInt (this UIColor color)
        {
            byte r, g, b, a;
            color.GetRGBA (out r, out g, out b, out a);
            return (r << 24) + (g << 16) + (b << 8) + a;
        }

        public static void GetRGBA (this UIColor color, out byte r, out byte g, out byte b, out byte a)
        {
            float rf, gf, bf, af;
            color.GetRGBA (out rf, out gf, out bf, out af);
            r = (byte)(rf * 255);
            g = (byte)(gf * 255);
            b = (byte)(bf * 255);
            a = (byte)(af * 255);
        }
        #endregion
        #region NSData
        public static byte[] ToArray (this NSData data)
        {
            if (data == null) {
                return null;
            }

            var ms = new MemoryStream ();
            data.AsStream ().CopyTo (ms);
            return ms.ToArray ();
        }

        public static UIImage ToUIImage (this NSData data)
        {
            return UIImage.LoadFromData (data);
        }
        #endregion
        #region byte[]
        public static UIImage ToUIImage(this byte[] data)
        {
            return UIImage.LoadFromData (NSData.FromArray (data));
        }
        #endregion
        #region UIImage
        public static UIImage ReduceSize (this UIImage image, float quality)
        {
            var data = image.AsJPEG (quality);
            return data.ToUIImage ();
        }
        #endregion
    }
}

