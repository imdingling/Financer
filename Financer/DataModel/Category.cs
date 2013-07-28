using System;
using SQLite;

namespace Financer
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Description { get; set; }

        public Category(string description)
        {
            this.Description = description;
        }

        public Category()
        {
        }
    }
}

