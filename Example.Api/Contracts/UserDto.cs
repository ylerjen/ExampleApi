using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Api.Contracts
{
    /// <summary>
    /// The Author object used for data transfert.
    /// Data validation is following the MS recommandation here <see cref="https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-2.1"/>
    /// </summary>
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Firstname { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}
