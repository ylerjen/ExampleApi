using System;
using System.IO;
using System.Reflection;

using AutoMapper;
using Example.Api.Commands;
using Example.Api.Contracts;
using Example.Business.Services;
using Example.Domain.Entities;
using Example.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

using Swashbuckle.AspNetCore.Examples;

using UnprocessableEntityObjectResult = Example.Api.Entities.UnprocessableEntityObjectResult;

namespace Example.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// The users controller handle all requests about the users.
    /// </summary>
    [Route("api/users")]
    [EnableCors("AllowSpecificOrigin")]
    public class UsersController : Controller
    {
        private IUsersService UsersServices { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="usersServices">
        /// The users services.
        /// </param>
        public UsersController(IUsersService usersServices)
        {
            this.UsersServices = usersServices;
        }

        /// <summary>
        /// Get the list of existing users. This list use a paging system.
        /// </summary>
        /// <param name="usersResourceParameter">This is the resource Parameter passed in the http header that are automatically set into this value by .net core</param>
        /// <returns>The list of users found</returns>
        [HttpGet]
        public IActionResult GetUserList(UsersResourceParameter usersResourceParameter)
        {
            var userList = this.UsersServices.GetUsersList(usersResourceParameter.PageNumber, usersResourceParameter.PageSize);
            return this.Ok(userList);
        }

        /// <summary>
        /// Get a single user by its Id
        /// </summary>
        /// <param name="id">The guid of the user <example>0ec5e125-5eee-4102-b48d-011a600fd74a</example></param>
        /// <returns>The <see cref="IActionResult"/> containing the user payload.</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            if (!this.UsersServices.UserExists(id))
            {
                return this.NotFound($"User with id {id} not found");
            }

            var user = this.UsersServices.GetUserById(id);
            var userDto = Mapper.Map<UserDto>(user);
            return this.Ok(userDto);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /users
        ///     {
        ///        "lastname": "Norris",
        ///        "firstname": "Chuck",
        ///        "birthdate": "1940-03-10"
        ///     }
        /// </remarks>
        /// <param name="userForCreationDto">The user to create</param>
        /// <returns>A http response</returns>
        [SwaggerRequestExample(typeof(UserForCreationDto), typeof(UserForCreationExample))]
        [SwaggerResponseExample(201, null)]
        [HttpPost]
        public IActionResult CreateUser([FromBody]UserForCreationDto userForCreationDto)
        {
            if (userForCreationDto == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(this.ModelState);
            }

            var user = Mapper.Map<User>(userForCreationDto);
            this.UsersServices.CreateUser(user);

            return this.Created($"{this.Request.Path.Value}/{user.Id}", user);
        }
    }

    /// <summary>
    /// An example for the user creation example payload.
    /// </summary> 
    /// <seealso cref="T:Swashbuckle.AspNetCore.Examples.IExamplesProvider" />
    public class UserForCreationExample : UserForCreationDto, IExamplesProvider
    {
        public UserForCreationExample()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("Example.Api.Swagger.Payloads.UserForCreationDto.json"));
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