using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

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
        public IHttpActionResult Movies()
        {
            var moviesDto = _dbContext.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDto);
        }

        [HttpGet]
        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        //POST /api/movies
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        //PUT /api/movies/1
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDto, movieInDb);
            movieInDb.NumberAvailable = movieDto.NumberInStock;
            _dbContext.SaveChanges();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        //DELETE /api/movies/1
        public void DeleteMovie(int id)
        {
            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Movies.Remove(movieInDb);
            _dbContext.SaveChanges();
        }
    }
}
