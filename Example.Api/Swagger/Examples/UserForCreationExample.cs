using System.IO;
using System.Reflection;

using Example.Api.Commands;

using Newtonsoft.Json.Linq;

using Swashbuckle.AspNetCore.Examples;

namespace Example.Api.Swagger.Examples
{
    /// <summary>
    /// An example for the user creation example payload.
    /// </summary> 
    /// <seealso cref="T:Swashbuckle.AspNetCore.Examples.IExamplesProvider" />
    public class UserForCreationExample : UserForCreationDto, IExamplesProvider
    {
        /// <summary>
        /// The path to the json file which contains the request body example
        /// </summary>
        private const string ExampleJsonFilePath = "Example.Api.Swagger.Payloads.UserForCreationDto.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserForCreationExample"/> class.
        /// </summary>
        public UserForCreationExample()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var textStreamReader = new StreamReader(assembly.GetManifestResourceStream(ExampleJsonFilePath));
            this.Data = textStreamReader.ReadToEnd();
        }

        /// <inheritdoc />
        public object GetExamples()
        {
            return JObject.Parse(this.Data).ToObject<UserForCreationDto>();
        }

        public string Data { get; set; }
    }
}