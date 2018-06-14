using AutoMapper;
using ExampleApi.Models;
using ExampleBusiness;
using ExampleBusiness.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ActionResult GetAuthorList()
        {
            var authorList = this.authorsServices.GetAuthorList();
            return Ok(authorList);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetAuthor(Guid id)
        {
            if (id == null || !this.authorsServices.AuthorExists(id))
            {
                return BadRequest();
            }

            var author = this.authorsServices.GetAuthorById(id);
            var authorDto = Mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }
    }
}