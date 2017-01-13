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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }


        [HttpGet]
        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customersQuery = _dbContext.Customers.Include(c => c.MemberShipType);

            //var customersDto = _dbContext.Customers
            //    .Include(c => c.MemberShipType)
            //    .ToList()
            //    .Select(Mapper.Map<Customer, CustomerDto>);
            var x = customersQuery.Count();
            var customerDtos = customersQuery
                .ToList()
.Select(Mapper.Map<Customer, CustomerDto>);


            return Ok(customerDtos);
        }

        [HttpGet]
        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int Id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return NotFound();
            //throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        //POST /api/customers
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); //customerDto;
        }

        [HttpPut]
        //PUT /api/customers/1
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            Mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.MemberShipTypeId = customerDto.MemberShipTypeId;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        //DELETE /api/customers/1
        public void DeleteCustomer(int Id)
        {

            var customerInDb = _dbContext.Customers.SingleOrDefault(c => c.Id == Id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _dbContext.Customers.Remove(customerInDb);
            _dbContext.SaveChanges();
        }
    }
}
