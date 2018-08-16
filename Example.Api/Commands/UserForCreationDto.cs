using System;
using System.ComponentModel.DataAnnotations;

using Example.Domain.Entities;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Example.Api.Commands
{
    /// <summary>
    /// This is the User DTO object used to create a new 
    /// </summary>
    /// <remarks>
    /// Payload example
    ///     POST /users
    ///     {
    ///        "lastname": "Norris",
    ///        "firstname": "Chuck",
    ///        "birthdate": "1940-03-10",
    ///        "gender": "M"
    ///     }
    /// </remarks>
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

        /// <summary>
        /// Gets or sets the user's gender
        /// </summary>
        /// <value>M or F</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
