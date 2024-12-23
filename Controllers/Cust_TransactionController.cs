using Clgproject.Data;
using Clgproject.Models.CustTrans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clgproject.Controllers
{
    public class Cust_TransactionController : Controller
    {
        private ClgDbContext clgDbContext;

        public Cust_TransactionController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Cust_Transaction cust_Transaction)
        {
            var tran = new Cust_Transaction()
            {
                mode = cust_Transaction.mode,
                t_type = cust_Transaction.t_type,
                t_status = cust_Transaction.t_status

            };
            clgDbContext.cust_Transactions.Add(tran);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tran = clgDbContext.cust_Transactions.ToList();
            return View(tran);
        }

        [HttpGet]
        public IActionResult UpdateTransaction(int t_id)
        {
            var tran = clgDbContext.cust_Transactions.FirstOrDefault(m => m.t_id == t_id);
            if (tran != null)
            {
                var view = new Cust_Transaction()
                {
                    t_id = tran.t_id,
                    mode = tran.mode,
                    t_type = tran.t_type,
                    t_status = tran.t_status
                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateTransaction(Cust_Transaction cust_Transaction)
        {
            var tran = clgDbContext.cust_Transactions.Find(cust_Transaction.t_id);
            if (tran != null)
            {

                tran.mode = cust_Transaction.mode;
                tran.t_type = cust_Transaction.t_type;
                tran.t_status = cust_Transaction.t_status;

                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int t_id)
        {
            var tran = clgDbContext.cust_Transactions.Where(m => m.t_id == t_id).FirstOrDefault();
            if (tran != null)
            {
                clgDbContext.cust_Transactions.Remove(tran);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
