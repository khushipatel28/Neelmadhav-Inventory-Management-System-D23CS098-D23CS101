using Clgproject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Clgproject.Models.MainTrans;
using Microsoft.Extensions.Hosting;
using Clgproject.Models.Products;
using System.Net.NetworkInformation;

namespace Clgproject.Controllers
{
    public class MainTransactionController : Controller
    {

    private ClgDbContext clgDbContext;

  public MainTransactionController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        } 


    public IActionResult Add()
    {
        return View();
    }

        [HttpPost]
        public IActionResult Add(MainTransaction mainTransaction)
        {
            var tran = new MainTransaction()
            {
                mode = mainTransaction.mode,
                t_type = mainTransaction.t_type,
                t_status = mainTransaction.t_status

            };
            clgDbContext.MainTransactions.Add(tran);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
            [HttpGet]
    public IActionResult Index()
    {
        var tran = clgDbContext.MainTransactions.ToList();
        return View(tran);
    }

    [HttpGet]
    public IActionResult UpdateTransaction(int t_id)
    {
        var tran = clgDbContext.MainTransactions.FirstOrDefault(m => m.t_id == t_id);
        if (tran != null)
        {
            var view = new MainTransaction()
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
    public IActionResult UpdateTransaction(MainTransaction mainTransaction)
    {
        var tran = clgDbContext.MainTransactions.Find(mainTransaction.t_id);
        if (tran != null)
        {

                tran.mode = mainTransaction.mode;
                tran.t_type = mainTransaction.t_type;
                tran.t_status = mainTransaction.t_status;

            clgDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int t_id)
    {
        var tran = clgDbContext.MainTransactions.Where(m => m.t_id == t_id).FirstOrDefault();
        if (tran != null)
        {
            clgDbContext.MainTransactions.Remove(tran);
            clgDbContext.SaveChanges();

        }
        return RedirectToAction("Index");
    }

}
}
