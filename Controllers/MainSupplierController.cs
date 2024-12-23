using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clgproject.Data;

//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Clgproject.Models.Products;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using Clgproject.Models.MainSuppliers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Linq.Dynamic.Core;
using Clgproject.Models.MainTrans;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

using Clgproject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clgproject.Models.MainSuppliers;

namespace Clgproject.Controllers
{
    public class MainSupplierController : Controller
    {
        private readonly ClgDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public MainSupplierController(ClgDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        // Display the form for adding a new supplier
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(MainSupplier mainSupplier)
        {
            // Check if a supplier with the same name already exists
            var existingSupplier = _context.MainSuppliers
                .FirstOrDefault(s => s.mainsupp_name == mainSupplier.mainsupp_name);

            if (existingSupplier != null)
            {
                // Log the old values in the history table
                var history = new MainSupplierHistory
                {
                    MainSupplierId = existingSupplier.mainsupp_id,
                    Name = existingSupplier.mainsupp_name,
                    Contact = existingSupplier.mainsupp_contact,
                    Address = existingSupplier.mainsupp_address,
                    AccountNumber = existingSupplier.account_no,
                    TransactionDate = DateTime.Now,
                    TransactionId = existingSupplier.MainTransactionId,
                    Mode = "cash", // Update with actual mode value
                    Type = "money", // Update with actual type value
                    Status = "yes", // Update with actual status value
                    CreatedAt = existingSupplier.created_at
                };
                _context.MainSupplierHistories.Add(history);

                // Update the existing supplier record with new values
                existingSupplier.mainsupp_contact = mainSupplier.mainsupp_contact;
                existingSupplier.mainsupp_address = mainSupplier.mainsupp_address;
                existingSupplier.account_no = mainSupplier.account_no;
                existingSupplier.MainTransactionId = mainSupplier.MainTransactionId;
                existingSupplier.created_at = DateTime.Now;

                _context.SaveChanges();
            }
            else
            {
                // Add a new supplier if it doesn't exist
                mainSupplier.created_at = DateTime.Now;
                _context.MainSuppliers.Add(mainSupplier);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult History()
        {
            var history = _context.MainSupplierHistories.ToList();
            return View(history);
        }



        // Handle the post request to add or update a supplier
        //[HttpPost]
        //public IActionResult Add(MainSupplier mainSupplier)
        //{
        // Check if the supplier already exists in the database
        //var supplier = _context.MainSuppliers
        //                       .FirstOrDefault(s => s.mainsupp_name == mainSupplier.mainsupp_name &&
        //                                            s.mainsupp_contact == mainSupplier.mainsupp_contact);

        //var existingSupplier = _context.MainSuppliers
        //.FirstOrDefault(s => s.mainsupp_name.Equals(mainSupplier.mainsupp_name, StringComparison.OrdinalIgnoreCase));



        // If supplier doesn't exist, create a new one
        //    var existingSupplier = new MainSupplier
        //    {
        //        mainsupp_name = mainSupplier.mainsupp_name,
        //        mainsupp_contact = mainSupplier.mainsupp_contact,
        //        mainsupp_address = mainSupplier.mainsupp_address,
        //        MainTransactionId = mainSupplier.MainTransactionId,
        //        account_no = mainSupplier.account_no,
        //        created_at = DateTime.Now
        //    };

        //    _context.MainSuppliers.Add(existingSupplier);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        // List all suppliers
        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _context.MainSuppliers
                                    .Include(s => s.mainTransactions)  // Include transaction details
                                    .ToList();
            return View(suppliers);
        }

        // Display the form to update a specific supplier
        [HttpGet]
        public IActionResult UpdateSupplier(int mainsupp_id)
        {
            var supplier = _context.MainSuppliers.FirstOrDefault(s => s.mainsupp_id == mainsupp_id);

            if (supplier != null)
            {
                return View(supplier);
            }

            return RedirectToAction("Index");
        }

        // Handle the post request to update a supplier
        [HttpPost]
        public IActionResult UpdateSupplier(MainSupplier mainSupplier)
        {
            var supplier = _context.MainSuppliers.Find(mainSupplier.mainsupp_id);

            if (supplier != null)
            {
                supplier.mainsupp_name = mainSupplier.mainsupp_name;
                supplier.mainsupp_contact = mainSupplier.mainsupp_contact;
                supplier.mainsupp_address = mainSupplier.mainsupp_address;
                supplier.MainTransactionId = mainSupplier.MainTransactionId;
                supplier.account_no = mainSupplier.account_no;
                supplier.created_at = mainSupplier.created_at;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Handle supplier deletion
        [HttpGet]
        public IActionResult Delete(int mainsupp_id)
        {
            var supplier = _context.MainSuppliers.FirstOrDefault(s => s.mainsupp_id == mainsupp_id);

            if (supplier != null)
            {
                _context.MainSuppliers.Remove(supplier);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Manually trigger supplier update
        [HttpGet]
        public IActionResult RunSupplierUpdate()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClgDbContext>();

                // Perform automated supplier update logic
                var suppliers = dbContext.MainSuppliers.ToList();

                foreach (var supplier in suppliers)
                {
                    // Example: Update created_at if a certain condition is met
                    if (supplier.created_at < DateTime.Now.AddYears(-1))
                    {
                        supplier.created_at = DateTime.Now;
                        dbContext.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult GetSupplierDetails(string name)
        //{
        //    var supplier = _context.MainSuppliers
        //                           .FirstOrDefault(s => s.mainsupp_name.Equals(name, StringComparison.OrdinalIgnoreCase));

        //    if (supplier != null)
        //    {
        //        // Return supplier details as JSON
        //        return Json(new
        //        {
        //            mainsupp_contact = supplier.mainsupp_contact,
        //            mainsupp_address = supplier.mainsupp_address,
        //            account_no = supplier.account_no
        //        });
        //    }

        //    return Json(null); // Return null if not found
        //}

        [HttpGet]
        //public IActionResult CheckSupplier(string mainSupplierName)
        //{
        //    // Search for the supplier in the database
        //    var supplier = _context.MainSuppliers
        //        .FirstOrDefault(s => s.mainsupp_name.Equals(mainSupplierName, StringComparison.OrdinalIgnoreCase));

        //    if (supplier != null)
        //    {
        //        // If supplier exists, return the details in a JSON format
        //        var supplierInfo = new
        //        {
        //            supplierContact = supplier.mainsupp_contact,
        //            supplierAddress = supplier.mainsupp_address,
        //            transactionId = supplier.MainTransactionId,
        //            account_no = supplier.account_no,
        //            created_at = supplier.created_at
        //        };

        //        // Returning success with the supplier information
        //        return Json(new { exists = true, supplierInfo = supplierInfo });
        //    }

        //    // If no supplier is found, return exists = false
        //    return Json(new { exists = false });
        //}
        public JsonResult CheckSupplier(string mainSupplierName)
        {
            // Query database to check if supplier exists
            var supplier = _context.MainSuppliers
                .Where(s => s.mainsupp_name == mainSupplierName)
                .Select(s => new
                {
                    s.mainsupp_contact,
                    s.mainsupp_address,
                    s.MainTransactionId,
                    s.account_no,
                    s.created_at
                })
                .FirstOrDefault();

            if (supplier != null)
            {
                // Return success response with supplier data
                return Json(new { exists = true, supplierInfo = supplier });
            }

            // Return failure response (supplier doesn't exist)
            return Json(new { exists = false });
        }
    }
}

