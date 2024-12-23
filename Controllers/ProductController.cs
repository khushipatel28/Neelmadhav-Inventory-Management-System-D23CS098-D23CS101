
using Clgproject.Data;
using Microsoft.AspNetCore.Mvc;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Clgproject.Models.Products;
using Microsoft.Extensions.Hosting;


namespace Clgproject.Controllers
{
    public class ProductController : Controller
    {
        private ClgDbContext clgDbContext;

        public ProductController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            var prod = new Product()
            {

                Mainsupp_id = product.Mainsupp_id,
                date_of_order = product.date_of_order,
                order_details = product.order_details,
                cost = product.cost,
                quantity = product.quantity,
               
                o_address = product.o_address,

            };
            clgDbContext.Products.Add(prod);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {

            var prod = clgDbContext.Products.Include("mainSupplier").ToList();
            return View(prod);
        }

        [HttpGet]
        public IActionResult UpdateProduct(int order_id)
        {
            var prod = clgDbContext.Products.FirstOrDefault(m => m.order_id == order_id);
            if (prod != null)
            {
                var view = new Product()
                {
                    order_id = prod.order_id,
                    Mainsupp_id = prod.Mainsupp_id,
                    date_of_order = prod.date_of_order,
                    order_details = prod.order_details,
                    cost = prod.cost,
                    quantity = prod.quantity,
              
                    o_address = prod.o_address,
                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            var prod = clgDbContext.Products.Find(product.order_id);
            if (prod != null)
            {

                prod.Mainsupp_id = product.Mainsupp_id;
                prod.date_of_order = product.date_of_order;
                prod.order_details = product.order_details;
                prod.cost = product.cost;
                prod.quantity = product.quantity;
             
                prod.o_address = product.o_address;

                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int order_id)
        {
            var prod = clgDbContext.Products.Where(m => m.order_id == order_id).FirstOrDefault();
            if (prod != null)
            {
                clgDbContext.Products.Remove(prod);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}
