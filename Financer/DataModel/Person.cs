using System;
using SQLite;

namespace Financer
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsCurrentUser { get; set; }
    
        public Person(string name)
        {
            this.Name = name;
        }

        public Person()
        {
        }

        public override string ToString ()
        {
            return this.Name;
        }

        public bool ContainsSearchWord(string value)
        {
            if (string.IsNullOrEmpty (value)) {
                return true;
            }

            return this.Name.Contains (value, StringComparison.OrdinalIgnoreCase);
        }

    }
}

