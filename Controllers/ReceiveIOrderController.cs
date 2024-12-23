using Clgproject.Data;
using Clgproject.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Clgproject.Controllers
{
    public class ReceiveIOrderController : Controller
    {
        private ClgDbContext clgDbContext;

        public ReceiveIOrderController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReceiveIOrder receiveIOrder)
        {
            var r_order = new ReceiveIOrder()
            {
                Order_id = receiveIOrder.Order_id,

                receive_datetime = receiveIOrder.receive_datetime

    };
            clgDbContext.receiveIOrders.Add(r_order);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var r_order = clgDbContext.receiveIOrders.Include("product").ToList();
            return View(r_order);
        }

        [HttpGet]
        public IActionResult UpdateReceiveIOrder(int receive_id)
        {
            var r_order = clgDbContext.receiveIOrders.FirstOrDefault(m => m.receive_id == receive_id);
            if (r_order != null)
            {
                var view = new ReceiveIOrder()
                {
                    receive_id= r_order.receive_id,
                   Order_id = r_order.Order_id,
                    receive_datetime = r_order.receive_datetime
                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateReceiveIOrder(ReceiveIOrder receiveIOrder)
        {
            var r_order = clgDbContext.receiveIOrders.Find(receiveIOrder.receive_id);
            if (r_order != null)
            {
                r_order.receive_id = receiveIOrder.receive_id;
                r_order.Order_id = receiveIOrder.Order_id;
                r_order.receive_datetime = receiveIOrder.receive_datetime;



                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int receive_id)
        {
            var r_order = clgDbContext.receiveIOrders.Where(m => m.receive_id == receive_id).FirstOrDefault();
            if (r_order != null)
            {
                clgDbContext.receiveIOrders.Remove(r_order);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}

