using System;
using System.Linq;
using System.Collections.Generic;

namespace Financer
{
    public static class ExtensionMethods
    {
        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            return source.IndexOf (value, comparison) >= 0;
        }

        public static bool In<T>(this T value, IEnumerable<T> collection)
        {
            return collection.Contains (value);
        }

        public static bool In<T>(this T value, params T[] values)
        {
            return values.Contains (value);
        }
    }
}

