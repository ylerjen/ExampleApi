using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Api.Commands
{
    public class UserForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }
    }
}
