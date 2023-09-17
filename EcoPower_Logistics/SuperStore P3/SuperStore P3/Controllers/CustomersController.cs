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
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View(_customerRepository.GetAllOrders());
        }

        public IActionResult Details(int id)
        {
            Customer customer = _customerRepository.GetItemById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                _customerRepository.AddItem(customer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Add a custom error message to ModelState.
                ModelState.AddModelError("ServiceId", ex.Message); 
                return View(customer);
            }

        }

        public IActionResult Edit(int id)
        {
            Customer customer = _customerRepository.GetItemById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.UpdateItem(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            Customer customer = _customerRepository.GetItemById(id);
            if (customer == null)
            {
                // Return a 404 error if the product is not found
                return NotFound(); 
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _customerRepository.DeleteItem(id);
            return RedirectToAction("Index");
        }

    }
}
