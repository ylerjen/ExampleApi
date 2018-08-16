using System.Runtime.Serialization;

namespace Example.Domain.Entities
{
    /// <summary>
    /// The enum which lists the supported genders for a person
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// The male gender.
        /// </summary>
        [EnumMember(Value = "M")]
        Male,

        /// <summary>
        /// The female gender.
        /// </summary>
        [EnumMember(Value = "F")]
        Female
    }
}
