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
    public class CustomerController : Controller
    {

        private ApplicationDbContext _dbContext;

        public CustomerController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _dbContext.Customers.Include(c => c.MemberShipType).ToList();
            return View(customers);
        }

        public ActionResult Detail(int Id)
        {
            var customer = _dbContext.Customers.Include(c=> c.MemberShipType).SingleOrDefault(c => c.Id == Id);
            if (customer != null)
            {
                return View(customer);
            }

            return HttpNotFound();
        }

        public ActionResult New()
        {
            return View();
        }
    }
}