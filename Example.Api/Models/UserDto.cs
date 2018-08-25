using System;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Example.Domain.Entities;

namespace Example.Api.Models
{
    /// <summary>
    /// The User object used for data transfert.
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
        
        /// <summary>
        /// Gets or sets the user's gender
        /// </summary>
        /// <value>M or F</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
