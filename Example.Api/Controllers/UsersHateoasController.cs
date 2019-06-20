using AutoMapper;
using Example.Api.Commands;
using Example.Api.Helpers;
using Example.Api.Models;
using Example.Business.Services;
using Example.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.Api.Controllers
{
    [Route("hateoas-api/users")]
    public class UsersHateoasController : Controller
    {        
        private readonly IUrlHelper urlHelper;
        private readonly IUsersService usersServices;

        public UsersHateoasController(IUsersService injectedUsersServices, IUrlHelper injectedUrlHelper)
        {
            this.usersServices = injectedUsersServices;
            this.urlHelper = injectedUrlHelper;
        }

        [HttpGet(Name = nameof(GetHateoasUserList))]
        public IActionResult GetHateoasUserList(ResourceParameter resourceParameter)
        {
            var skip = (resourceParameter.PageNumber > 0) ? resourceParameter.PageNumber - 1 : 0;
            var userList = Mapper.Map<IEnumerable<UserDto>>(this.usersServices.GetUsersList(skip, resourceParameter.PageSize));
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

            // add links on each retrieved user
            userList = userList.Select(user => this.CreateLinksForUser(user));

            var wrapper = new LinkedCollectionResourceWrapperDto<UserDto>(userList);

            return this.Ok(this.CreateLinksForUser(wrapper));
        }

        [HttpGet("{id}", Name = nameof(GetHateoasUser))]
        public IActionResult GetHateoasUser(Guid id)
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
            
            return this.Ok(this.CreateLinksForUser(userDto));
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
        [HttpPost(Name = nameof(CreateHateoasUser))]
        public IActionResult CreateHateoasUser([FromBody]UserForCreationDto userForCreationDto)
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

        [HttpPut("{id}", Name = nameof(ModifyHateoasUser))]
        public IActionResult ModifyHateoasUser(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id}", Name = nameof(PartiallyModifyHateoasUser))]
        public IActionResult PartiallyModifyHateoasUser(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}", Name = nameof(DeleteHateoasUser))]
        public IActionResult DeleteHateoasUser(Guid id)
        {
            throw new NotImplementedException();
        }

        private string CreateUserResourceUri(ResourceParameter param, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return this.urlHelper.Link(nameof(this.GetHateoasUserList), new
                    {
                        pageNumber = param.PageNumber - 1,
                        pageSize = param.PageSize
                    });
                case ResourceUriType.NextPage:
                    return this.urlHelper.Link(nameof(this.GetHateoasUserList), new
                    {
                        pageNumber = param.PageNumber + 1,
                        pageSize = param.PageSize
                    });
                default:
                    return this.urlHelper.Link(nameof(this.GetHateoasUserList), new
                    {
                        pageNumber = param.PageNumber,
                        pageSize = param.PageSize
                    });
            }
        }

        private UserDto CreateLinksForUser(UserDto user)
        {
            var idObj = new { id = user.Id };
            user.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.GetHateoasUser), idObj),
                "self",
                "GET"));

            user.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.ModifyHateoasUser), idObj),
                "update_user",
                "PUT"));

            user.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.PartiallyModifyHateoasUser), idObj),
                "partially_update_user",
                "PATCH"));

            user.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.DeleteHateoasUser), idObj),
                "delete_user",
                "DELETE"));

            return user;
        }
        
        private LinkedCollectionResourceWrapperDto<UserDto> CreateLinksForUser(
            LinkedCollectionResourceWrapperDto<UserDto> usersWrapper)
        {
            usersWrapper.Links.Add(
                new LinkDto(this.urlHelper.Link(nameof(this.GetHateoasUserList), new { }),
                "self",
                "GET"));

            return usersWrapper;
        }
    }
}
