using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        [HttpGet]
        //GET /api/movies/1
        public IEnumerable<CustomerDto> Movies()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        //POST /api/movies
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        //PUT /api/movies/1
        public void UpdateMovie(int id,MovieDto movieDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        //DELETE /api/movies/1
        public void deleteMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
