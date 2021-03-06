﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
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

        // GET: Movies
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Shrek!" };
            //return View(movie);
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();

            var customers = new List<Customer>
            {
                new Customer(){Name="Customer 1"},
                new Customer(){Name="Customer 2"}
            };

            var viewModel = new RandomMovieViewModel()
            {
                Customers = customers,
                Movie = movie
            };

            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
            return View(viewModel);
        }

        //public ActionResult Edit(int Id)
        //{
        //    return Content("ID = " + Id);
        //}

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (string.IsNullOrEmpty(sortBy))
                sortBy = "name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year:range(2016,2017)}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, byte month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Lista()
        {
            //var movies = _dbContext.Movies.Include(m => m.Genre).ToList();
            //MoviesViewModel viewModel = new MoviesViewModel();
            //viewModel.Movies = new List<Movie>();
            //viewModel.Movies.Add(new Movie() { Name = "Shrek", Id = 1 });
            //viewModel.Movies.Add(new Movie() { Name = "Wall-e", Id = 2 });

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("Lista");
            
            return View("ReadOnlyLista");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int Id)
        {
            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            if (movie != null)
                return View(movie);
            else return HttpNotFound();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Genres = _dbContext.Genres.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _dbContext.Genres.ToList()
                };

                return View("New", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock;
                _dbContext.Movies.Add(movie);
            }
            else
            {
                Movie movieInDb = _dbContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberAvailable = movie.NumberInStock;
            }

            try
            {

                _dbContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return RedirectToAction("Lista", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int Id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == Id);
            if (movie == null)
                return HttpNotFound();

            MovieFormViewModel viewModel = new MovieFormViewModel(movie)
            {
                Genres = _dbContext.Genres.ToList()
            };
            return View("New",viewModel);
        }
    }
}