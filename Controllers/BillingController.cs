using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Billing_System.Models;

namespace Billing_System.Controllers
{
    public class BillingController : Controller
    {
        // Static list to store products
        static List<Product> products = new List<Product>();

        // Show all products
        public IActionResult Index()
        {
            return View(products);
        }

        // Open Add Product page
        public IActionResult AddProduct()
        {
            return View();
        }

        // Handle form submit
        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            p.Id = products.Any() ? products.Max(x => x.Id) + 1 : 1;
            products.Add(p);
            return RedirectToAction("Index");
        }

        // Generate Bill
        public IActionResult Bill()
        {
            int total = products.Sum(x => x.Price * x.Quantity);
            ViewBag.Total = total;

            return View(products);
        }

        public IActionResult Delete(int id)
        {
            var item = products.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                products.Remove(item);
            }
            return RedirectToAction("Index");
        }
    }
}