using System;
using AutoMapper;
using Example.Api.Commands;
using Example.Api.Helpers;
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
            return Ok(authorList);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            if (!this.AuthorsServices.AuthorExists(id))
            {
                return NotFound($"Author with id {id} not found");
            }

            var author = this.AuthorsServices.GetAuthorById(id);
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
                this.AuthorsServices.CreateAuthor(author);
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