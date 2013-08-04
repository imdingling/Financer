using System;
using SQLite;
using MonoTouch.UIKit;

namespace Financer
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ColorInt { get; set; }

        [Ignore]
        public UIColor Color {
            get {
                return this.ColorInt.ToColor ();
            }
            set {
                this.ColorInt = value.ToInt ();
            }
        }

        public Category(string name, string description, UIColor color)
        {
            this.Name = name;
            this.Description = description;
            this.Color = color;
        }

        public Category()
        {
        }

        public bool ContainsSearchWord(string value)
        {
            if (string.IsNullOrEmpty (value)) {
                return true;
            }

            return this.Name.Contains (value, StringComparison.OrdinalIgnoreCase) ||
                   this.Description.Contains (value, StringComparison.OrdinalIgnoreCase);
        }
    }
}

