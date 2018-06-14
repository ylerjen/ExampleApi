using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
