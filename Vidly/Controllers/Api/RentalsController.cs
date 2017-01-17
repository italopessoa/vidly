using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
         private ApplicationDbContext _dbContext;
         public RentalsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        [HttpGet]
        //GET /api/rentals
        public IHttpActionResult GetRentals()
        {
            return BadRequest("Not implemented!");
        }

        [HttpGet]
        //GET /api/rentals/1
        public IHttpActionResult GetRental(int id)
        {
            return BadRequest("Not implemented!");
        }

        [HttpPost]
        //POST /api/rentals
        public IHttpActionResult CreateRental(Rental rental)
        {
            return BadRequest("Not implemented!");
        }

        [HttpPut]
        //PUT /api/rentals/1
        public IHttpActionResult CreateRental(int id, Rental rental)
        {
            return BadRequest("Not implemented!");
        }

        [HttpDelete]
        //DELETE /api/rentals/1
        public IHttpActionResult CreateRental(int id)
        {
            return BadRequest("Not implemented!");
        }

    }
}
