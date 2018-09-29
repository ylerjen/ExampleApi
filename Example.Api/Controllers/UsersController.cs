using System;
using System.Collections.Generic;

using AutoMapper;
using Example.Api.Commands;
using Example.Api.Helpers;
using Example.Api.Models;
using Example.Api.Swagger.Examples;
using Example.Business.Services;
using Example.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Examples;

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
        private readonly IUrlHelper urlHelper;
        private readonly IUsersService usersServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="injectedUsersServices">The users services.</param>
        /// <param name="injectedUrlHelper">The url helper from the dotnet framework</param>
        public UsersController(IUsersService injectedUsersServices, IUrlHelper injectedUrlHelper)
        {
            this.usersServices = injectedUsersServices;
            this.urlHelper = injectedUrlHelper;
        }

        /// <summary>
        /// Get the list of existing users.
        /// This list use a paging system which is automatically serialized to <see cref="ResourceParameter"/> by dotnetcore.
        /// </summary>
        /// <param name="resourceParameter">This is the resource Parameter passed in the http header that are automatically set into this value by .net core</param>
        /// <returns>The list of users found</returns>
        [HttpGet(Name = nameof(GetUserList))]
        public IActionResult GetUserList(ResourceParameter resourceParameter)
        {
            var userList = Mapper.Map<IEnumerable<UserDto>>(this.usersServices.GetUsersList(resourceParameter.PageNumber, resourceParameter.PageSize));
            var pagedList = PagedList<UserDto>.Create(
                userList,
                resourceParameter.PageNumber,
                resourceParameter.PageSize);
            var previousPageLink = pagedList.HasPrevious
                ? this.CreateUserResourceUri(resourceParameter, ResourceUriType.PreviousPage)
                : null;
            var nextPageLink = pagedList.HasNext
                ? this.CreateUserResourceUri(resourceParameter, ResourceUriType.NextPage)
                : null;

            var paginationMetadata = new PaginationMetadata(
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                previousPageLink,
                nextPageLink);

            this.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return this.Ok(pagedList);
        }

        private string CreateUserResourceUri(ResourceParameter param, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return this.urlHelper.Link(nameof(this.GetUserList), new
                    {
                        pageNumber = param.PageNumber - 1,
                        pageSize = param.PageSize
                    });
                case ResourceUriType.NextPage:
                    return this.urlHelper.Link(nameof(this.GetUserList), new
                    {
                        pageNumber = param.PageNumber + 1,
                        pageSize = param.PageSize
                    });
                default:
                    return this.urlHelper.Link(nameof(this.GetUserList), new
                    {
                        pageNumber = param.PageNumber,
                        pageSize = param.PageSize
                    });
            }
        }

        /// <summary>
        /// Get a single user by its Id
        /// </summary>
        /// <param name="id">The guid of the user <example>0ec5e125-5eee-4102-b48d-011a600fd74a</example></param>
        /// <returns>The <see cref="IActionResult"/> containing the user payload.</returns>
        [SwaggerResponseExample(201, typeof(UserDto))]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            if (!this.usersServices.UserExists(id))
            {
                return this.NotFound($"User with id {id} not found");
            }

            var user = this.usersServices.GetUserById(id);
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
            this.usersServices.CreateUser(user);

            return this.Created($"{this.Request.Path.Value}/{user.Id}", user);
        }
    }
}