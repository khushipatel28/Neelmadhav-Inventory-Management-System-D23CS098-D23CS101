using Clgproject.Data;
using Clgproject.Models.Suppliers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clgproject.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ClgDbContext clgDbContext;


        public SupplierController(ClgDbContext clgDbContext)
        {
            this.clgDbContext = clgDbContext;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Supplier supplier)
        {
            var supp = new Supplier()
            {

                name = supplier.name,
                address = supplier.address,
                contact = supplier.contact,

            };
            clgDbContext.Suppliers.Add(supp);
            clgDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var prod = clgDbContext.Suppliers.ToList();
            return View(prod);
        }

        [HttpGet]
        public IActionResult UpdateSupplier(int id)
        {
            var supp = clgDbContext.Suppliers.FirstOrDefault(m => m.id == id);
            if (supp != null)
            {
                var view = new Supplier()
                {
                    id = supp.id,
                    name = supp.name,
                    address = supp.address,
                    contact = supp.contact,

                };
                return View(view);

            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateSupplier(Supplier supplier)
        {
            var supp = clgDbContext.Suppliers.Find(supplier.id);
            if (supp != null)
            {

                supp.id = supplier.id;
                supp.name = supplier.name;
                supp.address = supplier.address;
                supp.contact = supplier.contact;

                clgDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var supp = clgDbContext.Suppliers.Where(m => m.id == id).FirstOrDefault();
            if (supp != null)
            {
                clgDbContext.Suppliers.Remove(supp);
                clgDbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }

    }
}