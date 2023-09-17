using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
using EcoPower_Logistics.Repository;

namespace Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View(_orderRepository.GetAllOrders());
        }

        public IActionResult Details(int id)
        {
            Order order = _orderRepository.GetItemById(id);
            if (order == null)
            {
                return NotFound();
            }
            var customers = _orderRepository.GetAllOrders();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        //Get
        public IActionResult Create()
        {
            var customers = _orderRepository.GetAllOrders();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                _orderRepository.AddItem(order);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Add a custom error message to ModelState.
                ModelState.AddModelError("ServiceId", ex.Message);
                var customers = _orderRepository.GetAllOrders();
                ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId", order.CustomerId);
                return View(order);
            }

        }
        public IActionResult Edit(int id)
        {
            Order order = _orderRepository.GetItemById(id);
            if (order == null)
            {
                return NotFound();
            }

            var customers = _orderRepository.GetAllOrders();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                // Update the order using the repository
                _orderRepository.UpdateItem(order);

                // Redirect to the Index action
                return RedirectToAction("Index");
            }

            var customers = _orderRepository.GetAllOrders();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "CustomerId", order.CustomerId);
            // If ModelState is not valid, return to the Edit view with the order model
            return View(order);
        }


        public IActionResult Delete(int id)
        {
            Order order = _orderRepository.GetItemById(id);
            if (order == null)
            {
                // Return a 404 error if the product is not found
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderRepository.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}
