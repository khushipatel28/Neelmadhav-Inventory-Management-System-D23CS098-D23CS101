using Clgproject.Data;
using Clgproject.Models.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using  Microsoft.EntityFrameworkCore;


namespace Clgproject.Controllers
{
    public class ReturnCOrderController : Controller
    {

        private ClgDbContext clgDbContext;

        public ReturnCOrderController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ReturnCOrder returnCOrder)
        {
            var returni = new ReturnCOrder()
            {

                Order_id = returnCOrder.Order_id,
                address = returnCOrder.address,

                date_of_order = returnCOrder.date_of_order,
                date_of_return = returnCOrder.date_of_return,

                t_return = returnCOrder.t_return

            };
            clgDbContext.returnCOrders.Add(returni);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var returni = clgDbContext.returnCOrders.Include("orderC").ToList();
            return View(returni);
        }

        [HttpGet]
        public IActionResult UpdateReturnCOrder(int return_id)
        {
            var returni = clgDbContext.returnCOrders.FirstOrDefault(m => m.return_id == return_id);
            if (returni != null)
            {
                var view = new ReturnCOrder()
                {
                    return_id = returni.return_id,
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
        public IActionResult UpdateReturnCOrder(ReturnCOrder returnCOrder)
        {
            var returni = clgDbContext.returnCOrders.Find(returnCOrder.return_id);
            if (returni != null)
            {

                returni.return_id = returnCOrder.return_id;
                returni.Order_id = returnCOrder.Order_id;
                returni.address = returnCOrder.address;
                returni.date_of_order = returnCOrder.date_of_order;
                returni.date_of_return = returnCOrder.date_of_return;
                returni.t_return = returnCOrder.t_return;

                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int return_id)
        {
            var returni = clgDbContext.returnCOrders.Where(m => m.return_id == return_id).FirstOrDefault();
            if (returni != null)
            {
                clgDbContext.returnCOrders.Remove(returni);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
