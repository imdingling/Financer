using System;
using SQLite;

namespace Financer
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    
        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Person()
        {
        }

        public override bool Equals (object obj)
        {
            var person = obj as Person;
            if (person != null) {
                return person.FirstName.Equals (this.FirstName, StringComparison.OrdinalIgnoreCase) &&
                       person.LastName.Equals (this.LastName, StringComparison.OrdinalIgnoreCase);
            }
      
            return false;
        }

        public override string ToString ()
        {
            return string.Format ("{0} {1}", this.FirstName, this.LastName);
        }
    }
}

