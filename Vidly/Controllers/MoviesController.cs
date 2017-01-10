using System;
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

        public ActionResult Edit(int Id)
        {
            return Content("ID = " + Id);
        }

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
            var movies = _dbContext.Movies.Include(m => m.Genre).ToList();
            //MoviesViewModel viewModel = new MoviesViewModel();
            //viewModel.Movies = new List<Movie>();
            //viewModel.Movies.Add(new Movie() { Name = "Shrek", Id = 1 });
            //viewModel.Movies.Add(new Movie() { Name = "Wall-e", Id = 2 });

            return View(movies);
        }

        public ActionResult Details(int Id)
        {
            var movie = _dbContext.Movies.Include(m=>m.Genre).SingleOrDefault(m => m.Id == Id);
            if (movie != null)
                return View(movie);
            else return HttpNotFound();
        }
    }
}