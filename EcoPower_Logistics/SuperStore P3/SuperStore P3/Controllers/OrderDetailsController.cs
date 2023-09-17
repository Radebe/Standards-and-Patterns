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
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailRepository _orderDetailsRepository;

        public OrderDetailsController(IOrderDetailRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public IActionResult Index()
        {
            return View(_orderDetailsRepository.GetAllOrders());
        }

        public IActionResult Details(int id)
        {
            OrderDetail orderDetail = _orderDetailsRepository.GetItemById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_orderDetailsRepository.GetAllOrders(), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_orderDetailsRepository.GetAllOrders(), "ProductId", "ProductId");
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {
            try
            {
                _orderDetailsRepository.AddItem(orderDetail);

                ViewData["OrderId"] = new SelectList(_orderDetailsRepository.GetAllOrders(), "OrderId", "OrderId");
                ViewData["ProductId"] = new SelectList(_orderDetailsRepository.GetAllOrders(), "ProductId", "ProductId");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Add a custom error message to ModelState.
                ModelState.AddModelError("ServiceId", ex.Message);
                return View(orderDetail);
            }

        }
        public IActionResult Edit(int id)
        {
            OrderDetail orderDetail = _orderDetailsRepository.GetItemById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        [HttpPost]
        public IActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _orderDetailsRepository.UpdateItem(orderDetail);
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        public IActionResult Delete(int id)
        {
            OrderDetail orderDetail = _orderDetailsRepository.GetItemById(id);
            if (orderDetail == null)
            {
                // Return a 404 error if the product is not found
                return NotFound();
            }
            return View(orderDetail);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderDetailsRepository.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}
