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
            var customer = _dbContext.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == Id);
            if (customer != null)
            {
                return View(customer);
            }

            return HttpNotFound();
        }

        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),//add value to the id hidden field, so I don't need [Bind(Exclude="Id")]
                MemberShipTypes = _dbContext.MemberShipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MemberShipTypes = _dbContext.MemberShipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
                _dbContext.Customers.Add(customer);
            else
            {
                var customerInDb = _dbContext.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;

                //TryUpdateModel(customerInDb);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int Id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MemberShipTypes = _dbContext.MemberShipTypes
            };
            return View("CustomerForm", viewModel);
        }
    }
}