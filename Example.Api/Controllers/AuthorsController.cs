using System;
using AutoMapper;
using Example.Api.Commands;
using Example.Api.Models;
using Example.Business.Services;
using Example.Domain.Entities;
using Example.Domain.Validations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/authors")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthorsController : Controller
    {
        IAuthorsService AuthorsServices { get; }

        public AuthorsController(IAuthorsService authorsServices)
        {
            this.AuthorsServices = authorsServices;
        }

        [HttpGet]
        public IActionResult GetAuthorList()
        {
            var authorList = this.AuthorsServices.GetAuthorsList();
            return this.Ok(authorList);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }
            if (!this.AuthorsServices.AuthorExists(id))
            {
                return this.NotFound($"Author with id {id} not found");
            }

            var author = this.AuthorsServices.GetAuthorById(id);
            var authorDto = Mapper.Map<AuthorDto>(author);
            return this.Ok(authorDto);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody]AuthorForCreationDto authorForCreationDto)
        {
            if(authorForCreationDto == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return new Helpers.UnprocessableEntityObjectResult(this.ModelState);
            }

            var author = Mapper.Map<Author>(authorForCreationDto);
            this.AuthorsServices.CreateAuthor(author);

            return this.Created($"{this.Request.Path.Value}/{author.Id}", author);
        }
    }
}