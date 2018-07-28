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
            this.Id = id;
            this.Lastname = lastname;
            this.Firstname = firstname;
            this.Birthdate = birthdate;
        }

        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime Birthdate { get; set; }

        public string Descr { get; set; } = string.Empty;
    }
}
