using System;
using AutoMapper;
using Example.Api.Commands;
using Example.Api.Contracts;
using Example.Business.Services;
using Example.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetUserList()
        {
            var userList = this.UsersServices.GetUsersList();
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

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserForCreationDto userForCreationDto)
        {
            if(userForCreationDto == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return new Helpers.UnprocessableEntityObjectResult(this.ModelState);
            }

            var user = Mapper.Map<User>(userForCreationDto);
            this.UsersServices.CreateUser(user);

            return this.Created($"{this.Request.Path.Value}/{user.Id}", user);
        }
    }
}