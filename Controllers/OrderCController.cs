using Clgproject.Data;
using Clgproject.Models.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clgproject.Controllers
{
    public class OrderCController : Controller
    {

        private ClgDbContext clgDbContext;

        public OrderCController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(OrderC c_Order)
        {
            var prod = new OrderC()
            { 
                Supplier_id = c_Order.Supplier_id,
                date_of_order = c_Order.date_of_order,
                order_details = c_Order.order_details,
                cost = c_Order.cost,
                quantity = c_Order.quantity,
                o_address = c_Order.o_address,
            };
            clgDbContext.C_orders.Add(prod);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var prod = clgDbContext.C_orders.Include("supplier").ToList();
            return View(prod);
        }

        [HttpGet]
        public IActionResult UpdateOrder(int order_id)
        {
            var prod = clgDbContext.C_orders.FirstOrDefault(m => m.order_id == order_id);
            if (prod != null)
            {
                var view = new OrderC()
                {
                    order_id = prod.order_id,
                    Supplier_id = prod.Supplier_id,
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
        public IActionResult UpdateOrder(OrderC c_Order)
        {
            var prod = clgDbContext.C_orders.Find(c_Order.order_id);
            if (prod != null)
            {
                prod.Supplier_id = c_Order.Supplier_id;
                prod.date_of_order = c_Order.date_of_order;
                prod.order_details = c_Order.order_details;
                prod.cost = c_Order.cost;
                prod.quantity = c_Order.quantity;
                prod.o_address = c_Order.o_address;
                clgDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int order_id)
        {
            var prod = clgDbContext.C_orders.Where(m => m.order_id == order_id).FirstOrDefault();
            if (prod != null)
            {
                clgDbContext.C_orders.Remove(prod);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
