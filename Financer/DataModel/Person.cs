using System;
using SQLite;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Financer
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsCurrentUser { get; set; }
    
        public Person(string name)
        {
            this.Name = name;
        }

        public byte[] Image { get; set; }

        public UIImage UIImage {
            get {
                if (this.Image == null) {
                    return Sys.RandomPersonImage;
                } else {
                    return this.Image.ToUIImage ();
                }
            }
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

        public static Dictionary<char, Person[]> GetPeopleDictionary (IEnumerable<Person> people)
        {
            return people.GroupBy (person => person.Name [0]).OrderBy(gr => gr.Key).ToDictionary (gr => gr.Key, gr => gr.ToArray ());
        }
    }
}

