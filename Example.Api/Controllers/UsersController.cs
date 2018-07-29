using System;
using AutoMapper;
using Example.Api.Commands;
using Example.Api.Contracts;
using Example.Business.Services;
using Example.Domain.Entities;
using Example.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UnprocessableEntityObjectResult = Example.Api.Entities.UnprocessableEntityObjectResult;

namespace Example.Api.Controllers
{
    [Route("api/users")]
    [EnableCors("AllowSpecificOrigin")]
    public class UsersController : Controller
    {
        private IUsersService UsersServices { get; }

        public UsersController(IUsersService usersServices)
        {
            this.UsersServices = usersServices;
        }

        /// <summary>
        /// Get the list of existing users. This list use a paging system.
        /// </summary>
        /// <param name="usersResourceParameter"></param>
        /// <returns>The list of users found</returns>
        [HttpGet]
        public IActionResult GetUserList(UsersResourceParameter usersResourceParameter)
        {
            var userList = this.UsersServices.GetUsersList(usersResourceParameter.PageNumber, usersResourceParameter.PageSize);
            return this.Ok(userList);
        }

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
        ///
        ///     POST /users
        ///     {
        ///        "lastname": "Norris",
        ///        "firstname": "Chuck",
        ///        "birthdate": "1940-03-10"
        ///     }
        ///
        /// </remarks>
        /// <param name="userForCreationDto">The user to create</param>
        /// <returns>A http response</returns>
        [HttpPost]
        public IActionResult CreateUser([FromBody]UserForCreationDto userForCreationDto)
        {
            if(userForCreationDto == null)
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
}