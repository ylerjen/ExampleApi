using System;

namespace ExampleDomain.Entities
{
    public class Author
    {
        public Author()
        {

        }

        public Author(Guid id, string lastname, string firstname)
        {
            this.Id = id;
            this.Lastname = lastname;
            this.Firstname = firstname;
        }

        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Descr { get; set; }
    }
}
