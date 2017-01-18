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
    public class NewRentalsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var movies = dbContext.Movies.Where(
                    m => newRental.MoviesId.Contains(m.Id));

                var customer = dbContext.Customers.Single(
                    c => c.Id == newRental.CustomerId);

                foreach (Movie movie in movies)
                {
                    Rental rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };
                    movie.NumberAvailable--;
                    dbContext.Rentals.Add(rental);
                }

                dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
