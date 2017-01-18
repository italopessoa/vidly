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
                //if (newRental.MovieIds.Count == 0)
                //    return BadRequest("No movie Ids have been giver.");
                //defensive

                var customer = dbContext.Customers.Single(
                    c => c.Id == newRental.CustomerId);

                //if (customer == null)
                //    return BadRequest("Customer Id is not valid.");
                //defensive

                var movies = dbContext.Movies.Where(
                    m => newRental.MovieIds.Contains(m.Id)).ToList();

                //if (movies.Count != newRental.MovieIds.Count)
                //    return BadRequest("One or more MovieIds are invalid.");
                //defensive

                foreach (Movie movie in movies)
                {
                    if (movie.NumberAvailable == 0)
                        return BadRequest("Movie is not available");

                    movie.NumberAvailable--;

                    Rental rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };
                    dbContext.Rentals.Add(rental);
                }

                dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}
