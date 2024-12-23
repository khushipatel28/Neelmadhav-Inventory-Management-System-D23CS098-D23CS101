using Clgproject.Data;
using Clgproject.Models.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clgproject.Controllers
{
    public class ReceiveCOrderController : Controller
    {
        private ClgDbContext clgDbContext;

        public ReceiveCOrderController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReceiveCOrder receiveCOrder)
        {
            var r_order = new ReceiveCOrder()
            {
                Order_id = receiveCOrder.Order_id,

                receive_datetime = receiveCOrder.receive_datetime

            };
            clgDbContext.receiveCOrders.Add(r_order);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var r_order = clgDbContext.receiveCOrders.Include("orderC").ToList();
            return View(r_order);
        }

        [HttpGet]
        public IActionResult UpdateReceiveCOrder(int receive_id)
        {
            var r_order = clgDbContext.receiveCOrders.FirstOrDefault(m => m.receive_id == receive_id);
            if (r_order != null)
            {
                var view = new ReceiveCOrder()
                {
                    receive_id = r_order.receive_id,
                    Order_id = r_order.Order_id,
                    receive_datetime = r_order.receive_datetime
                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateReceiveCOrder(ReceiveCOrder receiveCOrder)
        {
            var r_order = clgDbContext.receiveCOrders.Find(receiveCOrder.receive_id);
            if (r_order != null)
            {
                r_order.receive_id = receiveCOrder.receive_id;
                r_order.Order_id = receiveCOrder.Order_id;
                r_order.receive_datetime = receiveCOrder.receive_datetime;



                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int receive_id)
        {
            var r_order = clgDbContext.receiveCOrders.Where(m => m.receive_id == receive_id).FirstOrDefault();
            if (r_order != null)
            {
                clgDbContext.receiveCOrders.Remove(r_order);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}
