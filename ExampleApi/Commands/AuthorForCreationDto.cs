using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleApi.Commands
{
    public class AuthorForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
