using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerViewModel
    {
        public List<Customer> Customers
        {
            get
            {
                List<Customer> customers = new List<Customer>();
                customers.Add(new Customer() { Id = 1, Name = "Italo" });
                customers.Add(new Customer() { Id = 2, Name = "Davila" });

                return customers;
            }
        }
    }
}