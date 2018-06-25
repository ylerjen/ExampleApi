using AutoMapper;
using Example.Api.Helpers;
using Example.Api.Models;
using Example.Api.Commands;
using Example.Business.Services;
using Example.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using Example.Domain.Validations;

namespace ExampleApi.Controllers
{
    [Produces("application/json")]
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private IAuthorsService authorsServices { get; set; }

        public AuthorController(IAuthorsService authorsServices)
        {
            this.authorsServices = authorsServices;
        }

        [HttpGet]
        public IActionResult GetAuthorList()
        {
            var authorList = this.authorsServices.GetAuthorsList();
            return Ok(authorList);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (!this.authorsServices.AuthorExists(id))
            {
                return NotFound("Author with id ${id} not found");
            }

            var author = this.authorsServices.GetAuthorById(id);
            var authorDto = Mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody]AuthorForCreationDto authorForCreationDto)
        {
            if(authorForCreationDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var author = Mapper.Map<Author>(authorForCreationDto);
            try
            {
                this.authorsServices.CreateAuthor(author);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return new UnprocessableEntityObjectResult(ModelState);
            }
            return Created($"{Request.Path.Value}/{author.Id}", author);
        }
    }
}