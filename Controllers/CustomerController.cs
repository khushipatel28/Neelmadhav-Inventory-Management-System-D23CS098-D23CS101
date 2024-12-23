using Clgproject.Data;
using Microsoft.AspNetCore.Mvc;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Clgproject.Models.Customers;
using Microsoft.Extensions.Hosting;
using Clgproject.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;
using Clgproject.Models;
using Microsoft.AspNetCore.Http;

namespace Clgproject.Controllers
{
    public class CustomerController : Controller
    {
        private ClgDbContext clgDbContext;

        public CustomerController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            var cust = new Customer()
            {

                name = customer.name,
        cust_address =customer.cust_address,
                Cust_Transaction_id = customer.Cust_Transaction_id,
      account_no =customer.account_no
    };
            clgDbContext.Customers.Add(cust);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cust = clgDbContext.Customers.Include("cust_Transaction").ToList(); 
          
            return View(cust);
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int customer_id)
        {
            var cust = clgDbContext.Customers.FirstOrDefault(m => m.customer_id == customer_id);
            if (cust != null)
            {
                var view = new Customer()
                {
                    customer_id=cust.customer_id,
                    name = cust.name,
                    cust_address = cust.cust_address,
                    Cust_Transaction_id = cust.Cust_Transaction_id,
                    account_no = cust.account_no
                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var cust = clgDbContext.Customers.Find(customer.customer_id);
            if (cust != null)
            {

                cust.customer_id = customer.customer_id;
                cust.name = customer.name;
                cust.cust_address = customer.cust_address;
                cust.Cust_Transaction_id = customer.Cust_Transaction_id;
                cust.account_no = customer.account_no;

                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int customer_id)
        {
            var cust = clgDbContext.Customers.Where(m => m.customer_id == customer_id).FirstOrDefault();
            if (cust != null)
            {
                clgDbContext.Customers.Remove(cust);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
