using System;

namespace Example.Domain.Entities
{
    public class Author
    {
        public Author()
        {
        }

        public Author(Guid id, string lastname, string firstname, DateTime birthdate)
        {
            Id = id;
            Lastname = lastname;
            Firstname = firstname;
            Birthdate = birthdate;
        }

        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime Birthdate { get; set; }

        public string Descr { get; set; } = string.Empty;
    }
}
