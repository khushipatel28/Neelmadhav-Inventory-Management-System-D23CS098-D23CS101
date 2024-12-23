using Clgproject.Data;
using Clgproject.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Clgproject.Controllers
{
    public class ReturnIOrderController : Controller
    {

        private ClgDbContext clgDbContext;

        public ReturnIOrderController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReturnIOrder returnIOrder)
        {
            var returni = new ReturnIOrder()
            {

                 Order_id=returnIOrder.Order_id,
         address=returnIOrder.address,

       date_of_order =returnIOrder.date_of_order,
    date_of_return =returnIOrder.date_of_return,

         t_return =returnIOrder.t_return

    };
            clgDbContext.returnIOrders.Add(returni);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var returni = clgDbContext.returnIOrders.Include("product").ToList();
            return View(returni);
        }

        [HttpGet]
        public IActionResult UpdateReturnIOrder(int return_id)
        {
            var returni = clgDbContext.returnIOrders.FirstOrDefault(m => m.return_id == return_id);
            if (returni != null)
            {
                var view = new ReturnIOrder()
                {
                    return_id= returni.return_id,
                    Order_id = returni.Order_id,
                    address = returni.address,
                    date_of_order = returni.date_of_order,
                    date_of_return = returni.date_of_return,
                    t_return = returni.t_return
                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateReturnIOrder(ReturnIOrder returnIOrder)
        {
            var returni = clgDbContext.returnIOrders.Find(returnIOrder.return_id);
            if (returni != null)
            {

                returni.return_id = returnIOrder.return_id;
                returni.Order_id = returnIOrder.Order_id;
                returni.address = returnIOrder.address;
                returni.date_of_order = returnIOrder.date_of_order;
                returni.date_of_return = returnIOrder.date_of_return;
                returni.t_return = returnIOrder.t_return;

                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int return_id)
        {
            var returni = clgDbContext.returnIOrders.Where(m => m.return_id == return_id).FirstOrDefault();
            if (returni != null)
            {
                clgDbContext.returnIOrders.Remove(returni);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
