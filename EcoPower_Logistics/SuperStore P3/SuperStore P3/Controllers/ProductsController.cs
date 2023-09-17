using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using EcoPower_Logistics.Repository;

namespace Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_productRepository.GetAllOrders());
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            Product product = _productRepository.GetItemById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                _productRepository.AddItem(product);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Add a custom error message to ModelState.
                ModelState.AddModelError("ServiceId", ex.Message);
                return View(product);
            }

        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetItemById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.UpdateItem(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.GetItemById(id);
            if (product == null)
            {
                // Return a 404 error if the product is not found
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}
