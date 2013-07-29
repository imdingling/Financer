using System;
using SQLite;

namespace Financer
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category(string name, string description)
        {
            this.Name = name;
            this.Description = description;
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

